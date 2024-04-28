using System;
using System.Net.Http;
using System.Threading.Tasks;
using MicroService.Models;
using Newtonsoft.Json;

class HttpClientReq
{
    static async Task Main(string[] args)
    {
        // HTTP isteklerini göndermek için HttpClient oluştur
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5233/api/"); // Servisin adresini belirle


       // GET isteği gönder: Tüm müşterileri al
        var getResponseAll = await httpClient.GetAsync("Customer");
        if (getResponseAll.IsSuccessStatusCode)
        {
            var customersJson = await getResponseAll.Content.ReadAsStringAsync();
            //  var customers = JsonSerializer.Deserialize<Customer[]>(customersJson);
            Console.WriteLine("All Customers:");
            Console.WriteLine(JsonPrettyPrint(customersJson));
            
             //foreach (var customer in customersJson)
             //{
             //    //Console.Write(customer);
             //    Console.WriteLine(JsonConvert.SerializeObject(customer));
             //}
        }
        else
        {
            Console.WriteLine("Failed to get customers. Status code: " + getResponseAll.StatusCode);
        }

        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return string.Empty;

            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        // GET isteği gönder: Belirli ID
        var getResponse = await httpClient.GetAsync("Customer/4");
        if (getResponse.IsSuccessStatusCode)
        {
            // GET başarılı ise, müşteriyi al
            var customer = await getResponse.Content.ReadAsAsync<Customer>();
            Console.WriteLine("Customer with ID 4:");
            Console.WriteLine(JsonConvert.SerializeObject(customer));
        }
        else
        {
            Console.WriteLine("Failed to get customer. Status code: " + getResponse.StatusCode);
        }

        // Yeni müşteri oluştur
        var newCustomer = new Customer { Name = "John", Surname = "Doe", Email = "john@example.com" };

        // POST isteği gönder
        var postResponse = await httpClient.PostAsJsonAsync("Customer", newCustomer);
        if (postResponse.IsSuccessStatusCode)
        {
            // POST başarılı ise, oluşturulan müşteriyi al
            var createdCustomer = await postResponse.Content.ReadAsAsync<Customer>();
            Console.WriteLine("New customer created:");
            Console.WriteLine(JsonConvert.SerializeObject(createdCustomer));
        }
        else
        {
            Console.WriteLine("Failed to create customer. Status code: " + postResponse.StatusCode);
        }
      

        // PUT isteği gönder
        var updatedCustomer = new Customer { Id = 4, Name = "Updated Name",Surname="Name", Email = "updated@example.com" };
        var putResponse = await httpClient.PutAsJsonAsync("Customer/4", updatedCustomer);
        if (putResponse.IsSuccessStatusCode)
        {
            Console.WriteLine("Customer updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update customer. Status code: " + putResponse.StatusCode);
        }

        // DELETE isteği gönder
        var deleteResponse = await httpClient.DeleteAsync("Customer/5");
        if (deleteResponse.IsSuccessStatusCode)
        {
            Console.WriteLine("Customer deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete customer. Status code: " + deleteResponse.StatusCode);
        }
    }
}


//using System;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Text.Json;
//using System.Threading.Tasks;
//using MicroService.Models;
//using Newtonsoft.Json;

//class HttpClientReq
//{
//    static async Task Main(string[] args)
//    {
//        // HttpClient oluştur
//        using var httpClient = new HttpClient();
//        httpClient.BaseAddress = new Uri("http://localhost:5233/api/"); // Servis adresini belirle

//        try
//        {
            
//            // GET isteği gönder: Tüm müşterileri al
//            var getResponseAll = await httpClient.GetAsync("Customer");
//            if (getResponseAll.IsSuccessStatusCode)
//            {
//                var customersJson = await getResponseAll.Content.ReadAsStringAsync();
//              //  var customers = JsonSerializer.Deserialize<Customer[]>(customersJson);
//                Console.WriteLine("All Customers:");

//                Console.WriteLine(JsonPrettyPrint(customersJson));
//               /* foreach (var customer in customersJson)
//                {
//                    Console.Write(customer);
//                    //Console.WriteLine(JsonSerializer.Serialize(customer));
//                }*/
//            }
//            else
//            {
//                Console.WriteLine("Failed to get customers. Status code: " + getResponseAll.StatusCode);
//            }

//             static string JsonPrettyPrint(string json)
//            {
//                if (string.IsNullOrWhiteSpace(json))
//                    return string.Empty;

//                dynamic parsedJson = JsonConvert.DeserializeObject(json);
//                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
//            }



//            // GET isteği gönder: Belirli bir müşteriyi al
//            var getResponseById = await httpClient.GetAsync("Customer/4");
//            if (getResponseById.IsSuccessStatusCode)
//            {
//            var customerJson = await getResponseById.Content.ReadAsStringAsync();
//            if (!string.IsNullOrEmpty(customerJson))
//            {
//              // var customer = JsonSerializer.Deserialize<Customer>(customerJson);
//                Console.WriteLine("Customer with ID 4:");
//                Console.WriteLine(customerJson);
//            }
//            else
//            {
//                Console.WriteLine("Customer JSON is empty.");
//            }
//             }
//            else
//            {
//            Console.WriteLine("Failed to get customer by ID. Status code: " + getResponseById.StatusCode);
//             } 
         
//             // POST isteği gönder: Yeni bir müşteri ekle
//             var newCustomer = new Customer { Name = "John",Surname="Doe", Email = "john@example.com" };
//             var postResponse = await httpClient.PostAsJsonAsync("Customer", newCustomer);
//             if (postResponse.IsSuccessStatusCode)
//            {
//             var createdCustomerJson = await postResponse.Content.ReadAsStringAsync();
//            // var createdCustomer = JsonSerializer.Deserialize<Customer>(createdCustomerJson);
//             Console.WriteLine("New customer created:");
//           //  Console.WriteLine(JsonSerializer.Serialize(createdCustomer));
//             }
//            else
//            {
//             Console.WriteLine("Failed to create customer. Status code: " + postResponse.StatusCode);
//            }

//          // PUT isteği gönder: Belirli bir müşteriyi güncelle
//             var updatedCustomer = new Customer { Id = 4, Name = "Updated Name", Email = "updated@example.com" };
//             var putResponse = await httpClient.PutAsJsonAsync("Customer/4", updatedCustomer);
//             if (putResponse.IsSuccessStatusCode)
//             {
//             Console.WriteLine("Customer updated successfully.");
//             }
//             else
//             {
//             Console.WriteLine("Failed to update customer. Status code: " + putResponse.StatusCode);
//             }
    
//          // DELETE isteği gönder: Belirli bir müşteriyi sil
//             var deleteResponse = await httpClient.DeleteAsync("Customer/1");
//             if (deleteResponse.IsSuccessStatusCode)
//             {
//             Console.WriteLine("Customer deleted successfully.");
//             }
//            else
//             {
//                 Console.WriteLine("Failed to delete customer. Status code: " + deleteResponse.StatusCode);
//             }
//      }
//        catch (Exception ex)
//        {
//            Console.WriteLine("An error occurred while making the request: " + ex.Message);
//        }
//    }
//}
