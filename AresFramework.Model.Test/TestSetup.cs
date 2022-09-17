namespace AresFramework.Model.Test;

using ServiceDependencies;
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
        var builder = Host.CreateDefaultBuilder();
        AresServiceCollection.ServiceCollection = new ServiceCollection();
        AresServiceCollection.ServiceCollection.RegisterServices();

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