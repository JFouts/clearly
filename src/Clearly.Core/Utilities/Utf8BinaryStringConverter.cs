// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text;
using Clearly.Core.Utilities.Interfaces;

namespace Clearly.Core.Utilities;

public class Utf8BinaryStringConverter : IBinaryStringConverter
{
    public byte[] Encode(string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

    public string Decode(byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }
}
