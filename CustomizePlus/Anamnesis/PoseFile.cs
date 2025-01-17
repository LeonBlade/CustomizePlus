﻿// © Customize+.
// Licensed under the MIT license.

namespace Anamnesis.Files
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using CustomizePlus;
	using Newtonsoft.Json;

	[Serializable]
	public class PoseFile
	{
		public Dictionary<string, Bone?>? Bones { get; set; }

		[Serializable]
		public class Bone
		{
			public Vector? Scale { get; set; }
		}

		[Serializable]
		public class Vector
		{
			public float X { get; set; }
			public float Y { get; set; }
			public float Z { get; set; }

			public static Vector FromString(string str)
			{
				string[] parts = str.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

				if (parts.Length != 3)
					throw new FormatException();

				Vector v = new();
				v.X = float.Parse(parts[0], CultureInfo.InvariantCulture);
				v.Y = float.Parse(parts[1], CultureInfo.InvariantCulture);
				v.Z = float.Parse(parts[2], CultureInfo.InvariantCulture);
				return v;
			}
		}

		public class VectorConverter : JsonConverter<Vector>
		{
			public override Vector? ReadJson(JsonReader reader, Type objectType, Vector? existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				string? str = reader.Value as string;

				if (str == null)
					return null;

				return Vector.FromString(str);
			}

			public override void WriteJson(JsonWriter writer, Vector? value, JsonSerializer serializer)
			{
				throw new NotSupportedException();
			}
		}
	}
}
