# Crumb & Co. — Artisan Bakery

A complete small-business website built with C#, ASP.NET Core MVC (.NET 8), Razor HTML, CSS and a small amount of vanilla JavaScript. There is no database, shopping cart, online payment system or customer order storage.

## Open in Visual Studio 2022

Install Visual Studio 2022 version 17.8 or later with the **ASP.NET and web development** workload and .NET 8 SDK. Extract the supplied project ZIP, open `CrumbCo.sln`, select the `CrumbCo` launch profile, then press **F5**. The local profile runs at `http://localhost:5123`.

Alternatively, run `dotnet run --project CrumbCo.csproj` from the project folder. No npm installation or additional NuGet packages are required.

## What customers can do

Home introduces the bakery and featured bakes. About Us explains its story and artisan approach. Our Products displays eight illustrative products across Cakes, Bread, Cupcakes and Pastries, with category filtering and prices in South African rand. Gallery opens photographs in an accessible enlarged view. Contact shows business details and opening hours. Order Enquiries prepares a personalised WhatsApp message using the selected product, quantity, preferred date and optional details.

The enquiry form only prepares a message in the browser. Nothing is stored or submitted as a confirmed order. The customer reviews the message and chooses to open WhatsApp; availability, final pricing and collection are agreed with the bakery.

## Configure the bakery before going live

The name, contact information, address and prices are editable sample content, not a confirmed operating business. Update the `Bakery` section in `appsettings.json` with your real details. `WhatsAppNumber` must be your verified international business number with country code, without a local leading zero. Digits are recommended; a leading plus sign, spaces, parentheses and hyphens are accepted.

Leave `WhatsAppNumber` empty until you are ready. The site clearly explains preview mode and does not route sample enquiries to an invented phone number. Once the contact information is correct, set `IsDemo` to `false`. Set `PublicOrigin` to your real HTTPS domain to enable canonical and absolute social-preview URLs; do not use a local/internal URL.

Update opening hours in both `Views/Home/Contact.cshtml` and `Views/Shared/_Layout.cshtml`. Update prices, descriptions and products in `Services/ProductCatalog.cs`. Product photo styling, colours, typography and responsive rules are in `wwwroot/css/site.css`. The wordmark text lives in the shared layout; change it there as well when adapting this example to another business.

## Photos and portability

The Manus Preview version references five durable managed image paths. The downloadable Visual Studio ZIP includes local copies in `wwwroot/images` and replaces image references with local paths, so photography works without Manus services. The photographs are generated, representative bakery visuals rather than photographs of a real business’s products. Replace them with your own product photos before advertising exact designs. The second flavour in each category uses the category’s representative photograph.

Google Fonts are loaded online, with Georgia and Arial fallbacks if offline. All page content is server-rendered; navigation and product filtering work without JavaScript. Gallery enlargement and WhatsApp message preparation need JavaScript.

## Learn from the MVC structure

`Program.cs` registers MVC, typed business configuration and the in-memory product catalog. It also configures static assets, health checking and friendly error responses.

`Controllers/HomeController.cs` selects the page and supplies its view model. Each action has a readable URL: `/`, `/about`, `/products`, `/gallery`, `/contact` and `/enquiry`.

`Models/Product.cs` describes a product’s name, category, price, image and display information. `Models/BakerySettings.cs` contains editable business settings. `Services/ProductCatalog.cs` supplies products from a C# collection, not a database. `Services/WhatsAppLinks.cs` validates the configured recipient and URL-encodes messages.

`Views/Home` contains the six page templates. `Views/Shared/_Layout.cshtml` supplies the shared header, footer and metadata; `_ProductCard.cshtml` keeps product cards consistent. `wwwroot` contains CSS, JavaScript and the favicon. The downloadable ZIP also includes product photos.

To adapt this structure for a salon, restaurant or another small business, change the branding and information, rename the product categories and replace the images. Add a database, payments or online bookings only when the business needs them.

## Deployment

A multi-stage Dockerfile compiles the application using `mcr.microsoft.com/dotnet/sdk:8.0` and runs it with `mcr.microsoft.com/dotnet/aspnet:8.0`:

```bash
docker build -t crumbco .
docker run --rm -p 3000:3000 crumbco
```

The application honours `ASPNETCORE_URLS` when set and otherwise listens on `http://0.0.0.0:${PORT:-3000}`. `GET /_app/health` returns HTTP 200. Unknown page paths return a proper HTTP 404, and production failures use a friendly HTTP 500 page. HTTPS termination can be supplied by the hosting provider.
