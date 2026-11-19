using System;

namespace p48_lessons_asp.Data.Entity;

public class JwtToken
{
    public Guid Id{get;set;}
    public string Token{get;set;}
    public DateTime ExpirationDate{get;set;}
}
