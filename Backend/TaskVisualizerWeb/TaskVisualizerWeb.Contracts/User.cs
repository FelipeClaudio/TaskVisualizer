﻿namespace TaskVisualizerWeb.Contracts;

public readonly record struct User(string Name, string Email, UserStatusEnum Status);

public enum UserStatusEnum
{
    Active,
    Inactive,
}