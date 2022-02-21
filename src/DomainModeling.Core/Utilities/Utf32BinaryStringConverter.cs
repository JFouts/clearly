// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text;
using DomainModeling.Core.Utilities.Interfaces;

namespace DomainModeling.Core.Utilities;

public class Utf32BinaryStringConverter : IBinaryStringConverter
{
    public byte[] Encode(string str)
    {
        return Encoding.UTF32.GetBytes(str);
    }

    public string Decode(byte[] data)
    {
        return Encoding.UTF32.GetString(data);
    }
}
