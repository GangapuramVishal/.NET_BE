using System.Net.Http.Headers;
using System;
using System.Net.Http;
//using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text;

namespace TeamAdiDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Replace with your Microsoft Graph access token
            string accessToken = "eyJ0eXAiOiJKV1QiLCJub25jZSI6ImhGQnVwVzhfekt0WW1STDQzZ2RsekVxRTk1TXZ1V0tGSkVGOVVwc0pYQlkiLCJhbGciOiJSUzI1NiIsIng1dCI6InoxcnNZSEhKOS04bWdndDRIc1p1OEJLa0JQdyIsImtpZCI6InoxcnNZSEhKOS04bWdndDRIc1p1OEJLa0JQdyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC84ZjZiZDk4Mi05MmMzLTRkZTAtOTg1ZC0wZTI4N2M1NWUzNzkvIiwiaWF0IjoxNzM3MTEwMzA4LCJuYmYiOjE3MzcxMTAzMDgsImV4cCI6MTczNzE5NzAwOCwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFkUUFLLzhaQUFBQWl4RzNJZGRVVytSTmdsQ1Q1STh2SUw5V3NEM3JoaGgzRk4wWjhPMHBPNnBPUFgzdk0rWFRQZ0xoTVA1TS9hNGVDSWptWWY0V2Y3QU80bGRtU0VtckxQUEV4dlY4cW1yRWNGZHZsdUZ3RzJyWUlQa04wRUovWEpoTVNQaTJGR2Y3ZVlJUTZ0dnB5T09oSjdqSXFBRjVwamkrSnlSSG5RaTEzN1dlanhQSUYyY1dIQkVKTVg0OGRzWkpwblZTSFppOE1EbjNUcjFEa2UwOEo1MWQ0Uks0bzJ6ZWZRVHIzamNiSFh4eFhQVnVqeEZIM3U3ZEczU2hVQVhvQ0owRU8yZ2MrWm9VY294b0F5dENJZzhqWkkxZ0FBPT0iLCJhbXIiOlsicnNhIiwibWZhIl0sImFwcF9kaXNwbGF5bmFtZSI6IkdyYXBoIEV4cGxvcmVyIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJkZXZpY2VpZCI6IjEyZTEwYTNkLTRhZjEtNDJmYy05NDM5LWQzN2ZlMDYzOWQzMyIsImZhbWlseV9uYW1lIjoiVmlzaGFsIiwiZ2l2ZW5fbmFtZSI6IkdhbmdhcHVyYW0iLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIyNDAxOjQ5MDA6NGZmMTo4ZmE1OjdjYTM6Y2YwOjIzMjQ6YzA4YyIsIm5hbWUiOiJWaXNoYWwgR2FuZ2FwdXJhbSIsIm9pZCI6IjMyNDY1NGFiLTJjMmQtNDkxNS05ZmY2LTFlMGFlNThmYWFiMyIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMzU5QjgwRUIzIiwicmgiOiIxLkFWOEFndGxyajhPUzRFMllYUTRvZkZYamVRTUFBQUFBQUFBQXdBQUFBQUFBQUFCZkFFTmZBQS4iLCJzY3AiOiJEaXJlY3RvcnkuUmVhZC5BbGwgb3BlbmlkIFByZXNlbmNlLlJlYWQgcHJvZmlsZSBTaXRlcy5SZWFkV3JpdGUuQWxsIFVzZXIuUmVhZCBVc2VyLlJlYWQuQWxsIGVtYWlsIENhbGVuZGFycy5SZWFkIiwic2lnbmluX3N0YXRlIjpbImR2Y19tbmdkIiwiZHZjX2NtcCIsImttc2kiXSwic3ViIjoiTkdnUGpIcmdzWlViMDRUaFp4UWtFWlRGRHNJVFEtaXBmRkNRb0NmOTdERSIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJFVSIsInRpZCI6IjhmNmJkOTgyLTkyYzMtNGRlMC05ODVkLTBlMjg3YzU1ZTM3OSIsInVuaXF1ZV9uYW1lIjoiZ3Zpc2hhbEBhcmlxdC5jb20iLCJ1cG4iOiJndmlzaGFsQGFyaXF0LmNvbSIsInV0aSI6IlFQbXkySDNkRTBxcDVfRFFEMnQ5QUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbImI3OWZiZjRkLTNlZjktNDY4OS04MTQzLTc2YjE5NGU4NTUwOSJdLCJ4bXNfY2MiOlsiQ1AxIl0sInhtc19mdGQiOiJ6SW95YUtmM0pvcWZ0MnV6b0dsa1dfV19fNEhKXzY3NThudWtSQXE4MklBIiwieG1zX2lkcmVsIjoiMiAxIiwieG1zX3NzbSI6IjEiLCJ4bXNfc3QiOnsic3ViIjoiendZcEViM01KaXpDQXVLQmtjeFdiS2ZyejRTcHJIVEdkYVhLVXo3ZDcxQSJ9LCJ4bXNfdGNkdCI6MTU5MjgwNzY3MiwieG1zX3RkYnIiOiJFVSJ9.FQyY-5kXT0ZLwWl3Kj4A65dCHTSO6p2yW_36_kcSzHWIoLLb2btuNxB2CNSsSOqAHDlZxK60YmNUo-scLJood6TW7XjDVLpkz0mh-AuQqRIYZAWK_zVwE86R5iKxPKxiINFPWtWSW4GfyeMfUEfgpyrM9rh9Q7hzwqBBUwZoNjFHmYZArIlxwRjJwNm2Z0TAXotk-pw4oEypWo_RcnlA6QQbOEORmOx-pnOL53tA_7zEtabW68t790aH2BlDDw3TS32B_m_ZIL_nX-VYMvmGqHjQs1Xf78iDhDc_-It8IJoleEcHPmvZyWR3jolaxqZP70NZZvwXZVYYubBMjAoX6Q";

            // Microsoft Graph Calendar API endpoint
            string endpoint = "https://graph.microsoft.com/v1.0/me/events";

            try
            {
                // Create an HttpClient instance
                using (HttpClient client = new HttpClient())
                {
                    // Set the authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Make the GET request to the Graph API
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Calendar Events:");
                        Console.WriteLine(responseData);

                        var clientInfo = JsonSerializer.Deserialize<CalendarEvent>(responseData);
                        var events = new List<EventDetails>();

                        foreach (var info in clientInfo.value)
                        {
                            var subject = info.subject;
                            if (subject.Contains("Herman", StringComparison.OrdinalIgnoreCase))
                            {
                                EventDetails newEventDetails = new()
                                {
                                    Name = subject,
                                    InvoiceDate = info.start.dateTime,
                                };

                                events.Add(newEventDetails);
                            }

                            if (subject.Contains("Johnathan", StringComparison.OrdinalIgnoreCase))
                            {
                                EventDetails newEventDetails = new()
                                {
                                    Name = subject,
                                    InvoiceDate = info.start.dateTime,
                                };

                                events.Add(newEventDetails);
                            }
                        }

                        var token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImRBT1FKNnhPOVhnYk1nX0J5RzFoayJ9.eyJodHRwOi8vc25lbHN0YXJ0Lm5sL2NsaWVudElwIjoiMjQwMTo0OTAwOjY3NTc6OGFiMTplOWU2OjdiNzI6ZTE4MDo3NWQ1IiwiZGF0YSI6eyJhIjp7ImFjYWMiOiI5MDAwMjgiLCJhZGFjIjoiMTAwMDIwNiIsImFpZCI6IjI2NmYyNjBlLTQxYjQtNDNmOS1iNTk3LWJkMzg0N2Q4Y2JkYSIsImRuIjoiNzY4MDk5XzEwMDAyMDYiLCJsYXVyIjoiQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQkFRRUJBUUVCQVFFQiIsInBzIjoiL3c9PSIsInNzbiI6ImFkbWluaXN0cmF0aW9ucy0yMDE1LTA4LTExLWMxYjI5MGE2OWU0MjRhOWI4MWZhMGE1MzM3MWQ2YTI1In0sImFjIjoiOTAwMDI4IiwiYXIiOjIsInBzIjoiRHc9PSJ9LCJ2ZXJzaW9uIjoiMi4wLjAiLCJpc3MiOiJodHRwczovL2xvZ2luLXRzdC5zbmVsc3RhcnQubmwvIiwic3ViIjoiYXV0aDB8YWMxNDllMzktNGUwYS00MmEwLTkxMzctOGQyN2EyZjljZGU3IiwiYXVkIjpbImh0dHBzOi8vd2ViYXBpLXRzdC5zbmVsc3RhcnQubmwiLCJodHRwczovL3NuZWxzdGFydC10ZXN0LmV1LmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE3MzcxMTQzMzgsImV4cCI6MTczNzExNzkzOCwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCBvZmZsaW5lX2FjY2VzcyIsImF6cCI6ImlmMnBUN3libFF0VTgyeXFIZWJCVjJqeWlnZzJHSWV3In0.UaIcrpoPQz_t-_8x1kIiuVBA7LkAEIXbQ4GuLHUERTOw28whq7XHDz8-zZwX8U3yO0_0wmiAQfgWMjYC-61qHYUz5ng5JgHY1ADZGH0rO77utcOTqTCLNk_3EisrDmoYJXAN5z9njraGEcMdspMfiuJ84V_MAm1RQa8yZDjGNP91ghe3JImktIdFFevwl4WFzoGsRR1ZkX9y8YoNXARPLqvcd2o0qTMQOFrSWjOGJ4BPqBFvb6kLaEJq61DA1YAHswk2x2ss428T_djsjjarCjwIPwsRttzE2liZp3X0ZJyUOcwDPVJHaAZ6fvdrIr2JPLzVLOdknU2RTblj5Fr-zg";

                        foreach (var calenderEvent in events)
                        {
                            var hermanPayload =string.Empty;
                            var jonathanPayload = string.Empty;
                            if (calenderEvent.Name.Contains("Herman", StringComparison.OrdinalIgnoreCase))
                            {
                                hermanPayload = $@"
{{
  ""buyerId"": ""3131935c-535b-4231-b00c-0c34ecea6676"",
  ""creditDays"": 0,
  ""description"": ""Herman wants to sell these books"",
  ""discount"": 0,
  ""invoiceDate"": ""2025-01-17T10:36:13.203Z"",
  ""invoiceLines"": [
    {{
      ""description"": ""bahubali\r\n"",
      ""delivered"": 1,
      ""discount"": 0,
      ""lineIndex"": 1,
      ""productId"": ""1157fab6-63f6-4cf4-a0de-33d4d0ba516a"",
      ""totalQuantity"": 1,
      ""unitPrice"": 206.61,
      ""orderLineId"": ""f7f92a09-5664-4883-892a-981ea7100fd2"",
      ""isUnitPriceExclusiveVat"": true
    }}
  ],
  ""orderDate"": ""2025-01-17T10:34:49.352Z"",
  ""paymentReference"": """",
  ""sellerId"": ""ac149e39-4e0a-42a0-9137-8d27a2f9cde7"",
  ""templateId"": ""d4c1d9cb-03c4-44f4-9294-3bda5d835b65"",
  ""type"": ""DirectInvoice"",
  ""customerInfo"": {{
    ""shippingCustomer"": {{
      ""customerPublicId"": ""3131935c-535b-4231-b00c-0c34ecea6676"",
      ""name"": ""Herman SnelStart"",
      ""contactPerson"": """",
      ""street"": ""Alkmaar"",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }},
    ""invoicingCustomer"": {{
      ""customerPublicId"": ""3131935c-535b-4231-b00c-0c34ecea6676"",
      ""name"": ""Herman SnelStart"",
      ""contactPerson"": """",
      ""street"": ""Alkmaar"",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }}
  }},
  ""financialInfo"": {{
    ""paymentTermType"": ""None"",
    ""paymentConditionDiscountPercentage"": 0,
    ""paymentCondtionDays"": 0,
    ""costCenterIdentifier"": null,
    ""amountA"": 0,
    ""amountB"": 0,
    ""gAccountPercentage"": 0
  }},
  ""subscription"": {{}}
}}";
                            }

                            if (calenderEvent.Name.Contains("Johnathan", StringComparison.OrdinalIgnoreCase))
                            {
                                
                                     jonathanPayload = $@"
{{
  ""buyerId"": ""7bf861dd-672e-4c2d-b05a-9f4816119ba2"",
  ""creditDays"": 0,
  ""description"": ""15 books for sale"",
  ""discount"": 0,
  ""invoiceDate"": ""2025-01-17T10:36:13.203Z"",
  ""invoiceLines"": [
    {{
      ""description"": ""THE COLLAB"",
      ""delivered"": 1,
      ""discount"": 0,
      ""lineIndex"": 1,
      ""productId"": ""ab19220b-3245-4269-a46a-c0734f0cbe3e"",
      ""totalQuantity"": 1,
      ""unitPrice"": 123.97,
      ""orderLineId"": ""6a74e051-d3fd-4fb5-8e9e-ab29c624c594"",
      ""isUnitPriceExclusiveVat"": true
    }}
  ],
  ""orderDate"": ""{calenderEvent.InvoiceDate:yyyy-MM-ddTHH:mm:ss.fffZ}"",
  ""paymentReference"": """",
  ""sellerId"": ""ac149e39-4e0a-42a0-9137-8d27a2f9cde7"",
  ""templateId"": ""d4c1d9cb-03c4-44f4-9294-3bda5d835b65"",
  ""type"": ""DirectInvoice"",
  ""customerInfo"": {{
    ""shippingCustomer"": {{
      ""customerPublicId"": ""7bf861dd-672e-4c2d-b05a-9f4816119ba2"",
      ""name"": ""Jonathan SnelStart"",
      ""contactPerson"": """",
      ""street"": """",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }},
    ""invoicingCustomer"": {{
      ""customerPublicId"": ""7bf861dd-672e-4c2d-b05a-9f4816119ba2"",
      ""name"": ""Jonathan SnelStart"",
      ""contactPerson"": """",
      ""street"": """",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }}
  }},
  ""financialInfo"": {{
    ""paymentTermType"": ""None"",
    ""paymentConditionDiscountPercentage"": 0,
    ""paymentCondtionDays"": 0,
    ""costCenterIdentifier"": null,
    ""amountA"": 0,
    ""amountB"": 0,
    ""gAccountPercentage"": 0
  }},
  ""subscription"": {{}}
}}";
                            }




                            var apiUrl = "https://api-tst.snelstart.nl/product/payment/v1/invoice/create-invoice";

                            using var httpClient = new HttpClient();

                            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                            // Call API with Herman's payload
                            var hermanResponse = await PostInvoiceAsync(httpClient, apiUrl, hermanPayload);
                            Console.WriteLine($"Herman Response: {hermanResponse}");

                            // Call API with Jonathan's payload
                            var jonathanResponse = await PostInvoiceAsync(httpClient, apiUrl, jonathanPayload);
                            Console.WriteLine($"Jonathan Response: {jonathanResponse}");
                            // You can now use the clientInfo object
                            // Console.WriteLine($"Name: {clientInfo.Name}, Email: {clientInfo.Email}");
                        }
                    }

                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        string errorData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Details: {errorData}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task<string> PostInvoiceAsync(HttpClient client, string url, string payload)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }

    public class ClientInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        // Add other properties as needed
    }

    public class EventDetails
    {
        public string Id { get; set; }
        public string Name { get; set;}
        public DateTime InvoiceDate { get; set; }
    }

}
