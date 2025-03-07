using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;




    // NO ME SIRVE ESTA SINTAXIS POR EL TIPO DE ERROR QUE MANDA AL HACER EL REQUEST
    //public required string Username { get; set; }
    //public required string Password { get; set; }
    //    {
    //      "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    //      "title": "One or more validation errors occurred.",
    //      "status": 400,
    //      "errors": {
    //          "$": [
    //              "JSON deserialization for type 'API.DTOs.RegisterDto' was missing required properties, including the following: username; password"
    //          ],
    //          "registerDto": [
    //              "The registerDto field is required."
    //          ]
    //        },
    //          "traceId": "00-de5253de2926000e65f05976df4a628b-d2601157a168955c-00"
    //      }
}
