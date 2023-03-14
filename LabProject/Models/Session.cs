﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models;

public partial class Session
{
    public int SessionId { get; set; }

    [Required(ErrorMessage = "Номер сеансу обов'язкова")]
    [Display(Name = "Номер сеансу")]
    public string SessionNumber { get; set; }

    [Required(ErrorMessage = "Дата та час обов'язкові")]
    [Display(Name = "Дата, час")]
    public DateTime SessionDateTime { get; set; }

    public int HallId { get; set; }

    [Required(ErrorMessage = "Обов'язково оберіть фільм")]
    [Display(Name = "Фільм")]
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Обов'язково оберіть статус")]
    [Display(Name = "Статус")]
    public int StatusId { get; set; }

    [Display(Name = "Зала")]
    public virtual Hall Hall { get; set; }

    [Display(Name = "Фільм")]
    public virtual Movie Movie { get; set; }

    //[Required(ErrorMessage = "Обов'язково оберіть статус")]
    [Display(Name = "Статус")]
    public virtual Status Status { get; set; }
}
