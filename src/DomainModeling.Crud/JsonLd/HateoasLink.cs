// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.JsonLd;

public class HateoasLink
{
    public string Rel { get; set; }
    public string Link { get; set; }
    public string Method { get; set; }

    public HateoasLink(string rel, string link, string method = "GET")
    {
        Rel = rel;
        Link = link;
        Method = method;
    }
}
