using System;

namespace p48_lessons_asp.Services.Generator;

public interface IGenerator<T>
{
    List<T> Generate(int count);
}
