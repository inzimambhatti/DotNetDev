using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using virtualCoreApi.Entities;
using Npgsql;
using System.IO;
using Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace virtualCoreApi.Services
{
    public class MakePayment
    {
        public static async Task<dynamic> PayAsync(string cardnumber,int month,int year, string cvc,int value)
        {
            try
            {
                // var response = "";

                StripeConfiguration.ApiKey = "sk_test_51Mnw0jG2Bv2vyPgFdyTY1kYfMlT75Ri8fcSoiFAkEWb9aTKYE1c1H1XDEIRzTzPPsrcLa26jZqDp1TFxG3NXEhzD00Ut04lcnv";
                
                var optionstoken = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = cardnumber,
                        ExpMonth = month,
                        ExpYear = year,
                        Cvc = cvc 
                    }
                };

                var servicetoken = new TokenService();
                Token stripetoken = await servicetoken.CreateAsync(optionstoken);

                var options = new ChargeCreateOptions
                {
                    Amount = value,
                    Currency = "pkr",
                    Description = "test",
                    Source = stripetoken.Id
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);
                if (charge.Paid)
                {
                    return "Success";
                }
                else
                {
                    return "Failed";   
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        
    }
}