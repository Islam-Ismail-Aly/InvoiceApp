﻿global using InvoiceApp.API.Exceptions;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using InvoiceApp.API.Validations;
global using InvoiceApp.Core.Entities;
global using InvoiceApp.Core.Interfaces;
global using InvoiceApp.Core.Interfaces.Authentication;
global using InvoiceApp.Core.Validations.Authentication;
global using InvoiceApp.Infrastructure.Data;
global using InvoiceApp.Infrastructure.Services;
global using InvoiceApp.Infrastructure.Services.Authentication;
global using InvoiceApp.Infrastructure.UnitOfWork;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using System.Text;