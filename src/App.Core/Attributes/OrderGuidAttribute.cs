using System;

namespace App.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class OrderGuidAttribute : Attribute
{
    public bool Enable { get; set; } = true;
}