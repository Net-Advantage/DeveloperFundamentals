using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// This sets up the OpenId Connect with Azure AD B2C
var azureAdSection = configuration.GetRequiredSection("AzureAd");
services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApp(azureAdSection);

// This adds the Authorization 
//services.AddAuthorization(options =>
//{
//	options.AddPolicy("customPolicy", policy =>
//		policy.RequireAuthenticatedUser());
//});
services.AddAuthorization(options =>
{
	// By default, all incoming requests will be authorized according to the default policy.
	options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetRequiredSection("ReverseProxy"));
	//.AddTransforms(context =>
	//{
	//	//We do not want to forward our cookies to the api.
	//	context.RequestTransforms.Add(new RequestHeaderRemoveTransform("cookie"));
	//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();
//app.UseCookiePolicy();
//app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();