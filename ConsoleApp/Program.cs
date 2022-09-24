// See https://aka.ms/new-console-template for more information
using CreditInfo.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;
using CreditInfo.Service;
using CreditInfo.Domain;
using FluentValidation;

using IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices((_, services) =>
services.AddScoped<IContractProcessService, ContractProcessService>()
.AddScoped<IValidator<Contract>, ContractValidator>()
.AddGenericRepository<ContractContext>())
.Build();

ExemplifyScoping(host.Services, "Scope 1");
//ExemplifyScoping(host.Services, "Scope 2");

await host.RunAsync();

static async void ExemplifyScoping(IServiceProvider services, string scope)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    var processService = provider.GetRequiredService<IContractProcessService>();

    var contract = new Contract()
    {
        ContractCode = "123"
    };
    var res = await processService.ProcessContract(contract);
    
    Console.WriteLine(res.Status);

    Console.WriteLine();
}

