﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Profile_Service.Entities
{
    public class Themes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;
    }
}