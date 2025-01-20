﻿using System.ComponentModel.DataAnnotations;

namespace TowDriver.Infrastructure
{
    public record FindTowDriverByIdDto
    (
        [Required][RegularExpression(@"^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$", ErrorMessage = "Id must be a 'Guid'.")]
        string Id
    );
}