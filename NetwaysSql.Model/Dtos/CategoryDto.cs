﻿namespace NetwaysSql.Model
{
    using FluentValidation;

    public record CategoryDto
    {
        public Guid Id { get; init; }

        public required string Name { get; init; }

        public string? Description { get; init; }
    }
}
