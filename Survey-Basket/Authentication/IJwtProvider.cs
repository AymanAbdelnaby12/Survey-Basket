﻿using Survey_Basket.Models;

namespace SurveyBasket.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user);
    string ValidationToken(string token);
}