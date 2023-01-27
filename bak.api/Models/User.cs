﻿using System.Text.Json.Serialization;

namespace bak.api.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
