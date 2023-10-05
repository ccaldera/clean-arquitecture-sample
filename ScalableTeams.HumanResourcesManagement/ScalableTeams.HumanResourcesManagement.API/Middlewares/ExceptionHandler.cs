using Microsoft.AspNetCore.Http;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using FluentValidation;
using ScalableTeams.HumanResourcesManagement.API.Common.Models;

namespace ScalableTeams.HumanResourcesManagement.API.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    private static readonly Dictionary<Type, HttpStatusCode> s_codes = new()
    {
        { typeof(ResourceNotFoundException), HttpStatusCode.NotFound },
        { typeof(BusinessLogicException), HttpStatusCode.BadRequest },
        { typeof(AuthenticationException), HttpStatusCode.Unauthorized }
    };

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(httpContext, ex);
        }
        catch (BusinessLogicExceptions ex)
        {
            await HandleBusinessLogicExceptionsAsync(httpContext, ex);
        }
        catch (Exception ex) when (s_codes.ContainsKey(ex.GetType()))
        {
            await HandleApplicationExceptionsAsync(httpContext, ex);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        var response = new ValidationErrorResponse(ex.Errors);
        var content = JsonSerializer.Serialize(response);

        await WriteResponse(context, content, HttpStatusCode.BadRequest);
    }

    private static async Task HandleBusinessLogicExceptionsAsync(HttpContext context, BusinessLogicExceptions ex)
    {
        var response = new ValidationErrorResponse(ex.Errors);
        var content = JsonSerializer.Serialize(response);

        await WriteResponse(context, content, HttpStatusCode.BadRequest);
    }

    private static async Task HandleApplicationExceptionsAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = s_codes.GetValueOrDefault(ex.GetType());

        var response = new ErrorResponse(ex.Message);
        var content = JsonSerializer.Serialize(response);

        await WriteResponse(context, content, statusCode);
    }

    private static async Task WriteResponse(HttpContext context, string content, HttpStatusCode httpCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpCode;
        await context.Response.WriteAsync(content);
    }
}
