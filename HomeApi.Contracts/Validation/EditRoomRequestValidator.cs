using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;

namespace HomeApi.Contracts.Validation;

/// <summary>
/// Класс-валидатор запросов обновления комнаты
/// </summary>
public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
{
    /// <summary>
    /// Метод, конструктор, устанавливающий правила
    /// </summary>
    public EditRoomRequestValidator()
    {
        RuleFor(x => x.NewArea).GreaterThan(0);
        RuleFor(x => x.NewVoltage).GreaterThan(0);
        RuleFor(x => x.NewName).NotEmpty();
    }
}
