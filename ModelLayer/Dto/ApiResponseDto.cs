using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Dto
{
    public class ApiResponseDto<T>
    {
        public bool success { get; set; }
        public string message { get; set; }

        public T Data { get; set; }

        public string [] Errors { get; set; }

        // Making Sucess Response Controller
        public ApiResponseDto(bool success, string message,T Data) { 
            this.success = success;
            this.message = message;
            this.Data = Data;
        }
        //Error Response
        public ApiResponseDto(bool success,string message,params string[]errors ) {
            this.success = success;
            this.message = message;
            Errors = errors;
        }
        //ParameterLess Constructor
        public ApiResponseDto() { }
    }
}


/*
 We create a parameterless constructor mainly because:

✅ JSON serialization/deserialization needs it
✅ Frameworks use it while model binding/reflection
✅ Makes object creation flexible
✅ Avoids runtime errors in many cases
 */