﻿namespace caju_authorizer_domain.Authorizer.Entities
{
  public class Merchant
  {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string MCC { get; set; }
  }
}