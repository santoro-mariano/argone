/***********************************************
Author: Mariano Santoro
Description: The IApplicationBuilder interface
Created On: 02/28/2021
Modified By: 
Modified On:
Modified Comments: 
Ticket Number: 
************************************************/

namespace Argone.Core.Hosting
{
    using Argone.Core.Options;
    using Microsoft.Extensions.DependencyInjection;

    public interface IApplicationBuilder
    {
        ApplicationOptions Options { get; }
        
        IServiceCollection Services { get; }
    }
}