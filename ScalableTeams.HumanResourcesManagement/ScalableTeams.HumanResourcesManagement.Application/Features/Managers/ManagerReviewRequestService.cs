﻿using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResources.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.Managers;

public class ManagerReviewRequestService : IFeatureService<ManagerReviewRequest>
{
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IEmployeesRepository employeesRepository;
    private readonly IHumanResourcesNotificationService humanResourcesNotificationService;
    private readonly IEmployeesNotificationService employeesNotificationService;

    public ManagerReviewRequestService(
        IVacationsRequestRepository vacationsRequestRepository,
        IEmployeesRepository employeesRepository,
        IHumanResourcesNotificationService humanResourcesNotificationService,
        IEmployeesNotificationService employeesNotificationService)
    {
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.employeesRepository = employeesRepository;
        this.humanResourcesNotificationService = humanResourcesNotificationService;
        this.employeesNotificationService = employeesNotificationService;
    }

    public async Task Execute(ManagerReviewRequest input, CancellationToken cancellationToken)
    {
        var vacationsRequest = await vacationsRequestRepository.Get(input.VacationRequestId)
            ?? throw new ResourceNotFoundException($"Vacation request with id {input.VacationRequestId} was not found");

        var employee = await employeesRepository.Get(vacationsRequest.EmployeeId)
            ?? throw new ResourceNotFoundException($"The employee {vacationsRequest.EmployeeId} related to this request does not exists.");

        if (employee.ManagerId != input.ReviewerId)
        {
            throw new BusinessLogicException("Only the employee's manager can review this request.");
        }

        switch (input.NewStatus)
        {
            case ProcessStatus.ManagerApproves:
                vacationsRequest.ManagerApproves();
                break;
            case ProcessStatus.ManagerRejects:
                vacationsRequest.ManagerRejects();
                break;
            default:
                throw new BusinessLogicException("Managers can only approve or reject requests.");
        }

        await vacationsRequestRepository.SaveChanges();

        var notifications = new Task[] 
        {
            humanResourcesNotificationService.SendNewVacationRequestNotification(vacationsRequest, cancellationToken),
            employeesNotificationService.SendVacationRequestUpdateNotification(vacationsRequest, cancellationToken)
        };

        try
        {
            await Task.WhenAll(notifications);
        }
        catch (Exception)
        {
        }
    }
}
