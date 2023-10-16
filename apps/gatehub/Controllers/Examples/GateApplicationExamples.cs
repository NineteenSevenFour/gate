using NineteenSevenFour.Gatehub.Domain.Models;

using Swashbuckle.AspNetCore.Filters;

using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Gatehub.Controllers.NewFolder
{
  /// <summary>
  /// An example of GATE application registration request payload.
  /// </summary>
  [ExcludeFromCodeCoverage( Justification = "Swagger example model")]
  public class GateApplicationMetadataRegistrationModelExample : IExamplesProvider<GateApplicationMetadataRegistrationModel>
  {
    GateApplicationMetadataRegistrationModel IExamplesProvider<GateApplicationMetadataRegistrationModel>.GetExamples()
    {
      return new GateApplicationMetadataRegistrationModel()
      {
        Name = "MyDummyApp",
        Description = "Some dummy application description that does nothing",
        Icon = "cog"
      };
    }
  }

  /// <summary>
  /// An example of registered GATE application
  /// </summary>
  [ExcludeFromCodeCoverage(Justification = "Swagger example model")]
  public class GateApplicationMetadataModelExample : IExamplesProvider<GateApplicationMetadataModel>
  {
    GateApplicationMetadataModel IExamplesProvider<GateApplicationMetadataModel>.GetExamples()
    {
      return new GateApplicationMetadataModel()
      {
        Id = 4867,
        Name = "MyRegisteredDummyApp",
        Description = "Some registered dummy application description that does nothing",
        Icon = "cog"
      };
    }
  }

  /// <summary>
  /// An example of list of registered GATE applications
  /// </summary>
  [ExcludeFromCodeCoverage(Justification = "Swagger example model")]
  public class GateApplicationMetadataModelListExample : IExamplesProvider<GateApplicationMetadataModel[]>
  {
    GateApplicationMetadataModel[] IExamplesProvider<GateApplicationMetadataModel[]>.GetExamples()
    {
      return new GateApplicationMetadataModel[] {
        new ()
        {
          Id = 55,
          Name = "MyRegisteredDummyApp",
          Description = "Some registered dummy application description that does nothing",
          Icon = "cog"
        },
        new ()
        {
          Id = 3,
          Name = "MyOtherRegisteredDummyApp",
          Description = "Other registered dummy application description that does nothing",
          Icon = "plus"
        },
        new ()
        {
          Id = 5678,
          Name = "YetAnotherRegisteredDummyApp",
          Description = "Yet another registered dummy application description that does nothing",
          Icon = "user"
        }
      };
    }
  }
}
