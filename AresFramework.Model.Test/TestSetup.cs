using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AresFramework.Model.Test;

using ServiceDependency;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using GameEngine;

using Microsoft.Extensions.Hosting;

[SetUpFixture]
public class TestSetup
{

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        
        
        var iConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("settings.json")
            .Build();
        
        var builder = Host.CreateDefaultBuilder();
        AresServiceCollection.ServiceCollection = new ServiceCollection();
        AresServiceCollection.ServiceCollection.RegisterServices(iConfig);

        var serviceFactory = new DefaultServiceProviderFactory();
        serviceFactory.CreateBuilder(AresServiceCollection.ServiceCollection);
        builder.UseServiceProviderFactory(serviceFactory);
        AresServiceCollection.ServiceProvider = serviceFactory.CreateServiceProvider(AresServiceCollection.ServiceCollection);
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        
    }
    
}