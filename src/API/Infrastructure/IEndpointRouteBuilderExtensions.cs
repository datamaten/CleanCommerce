﻿using System.Diagnostics.CodeAnalysis;
using System;

namespace API.Infrastructure;

public static class IEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
    {
        builder.MapGet(pattern, handler)
            .WithName(handler.Method.Name)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }

    public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
    {
        builder.MapPost(pattern, handler)
            .WithName(handler.Method.Name)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }

    public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern)
    {
        builder.MapPut(pattern, handler)
            .WithName(handler.Method.Name)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }

    public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern)
    {
        builder.MapDelete(pattern, handler)
            .WithName(handler.Method.Name)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
