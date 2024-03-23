using InternetShop.Models;
using InternetShop.Services;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace InternetShop.Data
{
    public class Seed
    {
        public static async void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
     
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    //Roles=-------------------------------------

                    var userRole = new Roles { Name = UserRoles.User };
                    var adminRole = new Roles { Name = UserRoles.Admin };
                    var moderatorRole = new Roles { Name = UserRoles.Moderator };
                    await context.AddRangeAsync(userRole, adminRole, moderatorRole);

                    //Categories=--------------------------------

                    var laptops = new Categories { Name = "Ноутбуки", ImageUrl = "https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/noutbuki.png" };
                    var mobilePhones = new Categories { Name = "Мобільні телефони", ImageUrl = "https://content2.rozetka.com.ua/constructor/images_site/original/378644481.jpg" };
                    var washingMachines = new Categories { Name = "Пральні машини", ImageUrl = "https://content1.rozetka.com.ua/constructor/images_site/original/325502385.png" };
                    var armchairs = new Categories { Name = "Крісла", ImageUrl = "https://video.rozetka.com.ua/img_superportal/mebel/01.jpg" };
                    var screws = new Categories { Name = "Шурупокрути", ImageUrl = "https://content.rozetka.com.ua/goods/top/original/228454748.jpg" };
                    var kitchenSinks = new Categories { Name = "Кухонні мийки", ImageUrl = "https://content.rozetka.com.ua/goods/top/original/228454490.jpg" };
                    var chainSaws = new Categories { Name = "Ланцюгові пили", ImageUrl = "https://content.rozetka.com.ua/goods/top/original/228454783.jpg" };
                    var bicycles = new Categories { Name = "Велосипеди", ImageUrl = "https://content.rozetka.com.ua/goods/top/original/228453730.jpg" };
                    var bassGuitars = new Categories { Name = "Бас-гітари", ImageUrl = "https://content2.rozetka.com.ua/goods/top/original/228455015.jpg" };
                    var headphone = new Categories { Name = "Навушники", ImageUrl = "https://content1.rozetka.com.ua/goods/top/original/228453322.png" };

                    await context.AddRangeAsync(laptops, mobilePhones, washingMachines, armchairs, screws,
                        kitchenSinks, chainSaws, bicycles, bassGuitars, headphone);

                    //Delivery=----------------------------------

                    var ukrposhta = new Delivery { DeliveryType = "Ukrposhta" };
                    var novaposhta = new Delivery { DeliveryType = "Novaposhta" };

                    await context.AddRangeAsync(ukrposhta, novaposhta);

                    //Users=-------------------------------------

                    var CortC4Deluxe = new Products
                    {
                        Name = "Бас-гітара Cort C4 Deluxe",
                        Description = "Кількість струн: 4\r\nМензура: 34\"",
                        Price = 15675m,
                        Categories = bassGuitars,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/409648772.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/409648773.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/409648774.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/409648775.jpg"
                            },
                        }
                    };
                    var AppleIPhoneEarPods = new Products
                    {
                        Name = "Навушники Apple iPhone EarPods",
                        Description = "Тип навушників: Вкладки",
                        Price = 999m,
                        Categories = headphone,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/10813688.jpg"
                            },
                        }
                    };

                    await context.AddRangeAsync(CortC4Deluxe, AppleIPhoneEarPods);

                    var HPPavilion = new Products
                    {
                        Name = "Ноутбук HP Pavilion 15-eg3027ua",
                        Description = "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i5-1335U (3.4 - 4.6 ГГц) / RAM 16 ГБ / SSD 512 ГБ / Intel Iris Xe Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / DOS / 1.74 кг / сріблястий",
                        Price = 24999m,
                        Categories = laptops,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/375424609.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/375424610.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/375424611.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/375424612.jpg"
                            },
                        }
                    };
                    var SamsungGalaxyA34 = new Products
                    {
                        Name = "Мобільний телефон Samsung Galaxy A34",
                        Description = "Екран (6.6\", Super AMOLED, 2340x1080) / Mediatek Dimensity 1080 (2 x 2.6 ГГц + 6 x 2.0 ГГц) / основна потрійна камера: 48 Мп + 8 Мп + 5 Мп, фронтальна камера: 13 Мп/ RAM 8 ГБ / 256 ГБ вбудованої пам'яті + microSD (до 1 ТБ) / 3G / LTE / 5G / GPS / A-GPS / ГЛОНАСС / BDS / підтримка 2х SIM-карток (Nano-SIM) / Android 13 / 5000 мА * год",
                        Price = 12499m,
                        Categories = mobilePhones,
                        Images = new List<Images>
                                {
                                    new Images
                                    {
                                        Url = "https://content1.rozetka.com.ua/goods/images/big/319594401.jpg"
                                    },
                                    new Images
                                    {
                                        Url = "https://content.rozetka.com.ua/goods/images/big/319594402.jpg"
                                    },
                                    new Images
                                    {
                                        Url = "https://content2.rozetka.com.ua/goods/images/big/319594403.jpg"
                                    },
                                    new Images
                                    {
                                        Url = "https://content1.rozetka.com.ua/goods/images/big/319594405.jpg"
                                    },
                                    new Images
                                    {
                                        Url = "https://content2.rozetka.com.ua/goods/images/big/319594406.jpg"
                                    },
                                }
                    };

                    await context.AddRangeAsync(HPPavilion, SamsungGalaxyA34);

                    var user1 = new Users
                    {
                        Email = "ivanenko.oleksandr@example.com",
                        Password = PasswordManager.HashPassword("11AAivanenko"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380987654321",
                        Surname = "Іваненко",
                        Name = "Олександр",
                        Patronimic = "Петрович",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук Lenovo IdeaPad Gaming 3 15ACH6",
                                    Description = "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / AMD Ryzen 5 5500H (3.3 - 4.2 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 2050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.25 кг / чорний",
                                    Price = 29999m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/382257301.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/382257302.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/382257303.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/382257304.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/382257305.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук Acer Aspire 7 A715-76G-560W",
                                    Description = "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i5-12450H (2.0 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.1 кг / чорний",
                                    Price = 31999m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/362592851.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/372795220.png"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/362592861.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/362592869.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук ASUS Vivobook 15 X1500EA-BQ3733",
                                    Description = "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i3-1115G4 (3.0 - 4.1 ГГц) / RAM 12 ГБ / SSD 512 ГБ / Intel UHD Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / без ОС / 1.8 кг / сріблястий",
                                    Price = 16499m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/334343847.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/334343848.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/334343849.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/334343850.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук Apple MacBook Air 13\" M1 8/256GB 2020",
                                    Description = "Екран 13.3\" Retina (2560x1600) WQXGA, глянсовий / Apple M1 / RAM 8 ГБ / SSD 256 ГБ / Apple M1 Graphics / Wi-Fi / Bluetooth / macOS Big Sur / 1.29 кг / сірий",
                                    Price = 36499m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/144249716.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/144249735.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/144249755.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/144249835.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = HPPavilion,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Samsung Galaxy A24 6/128GB",
                                    Description = "Екран (6.5\", Super AMOLED, 2340x1080) / Mediatek Helio G99 (2 x 2.6 ГГц + 6 x 2.0 ГГц) / основна потрійна камера: 50 Мп + 5 Мп + 2 Мп, фронтальна камера: 13 Мп / RAM 6 ГБ / 128 ГБ вбудованої пам'яті + microSD (до 1 ТБ) / 3G / LTE / GPS / ГЛОНАСС / BDS / підтримка 2х SIM-карток (Nano-SIM) / Android 13 / 5000 мА*год",
                                    Price = 7599m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/328126511.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/328126512.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/328126513.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/328126514.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/328126516.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Apple iPhone 15 Pro Max",
                                    Description = "Екран (6.7\", OLED (Super Retina XDR), 2796x1290) / Apple A17 Pro / основна квадро-камера: 48 Мп + 12 Мп + 12 Мп + 12 Мп, фронтальна камера: 12 Мп / 256 ГБ вбудованої пам'яті / 3G / LTE / 5G / GPS / Nano-SIM / iOS 17",
                                    Price = 59999m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/364834195.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/364834187.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/364834202.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/364834208.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/364834215.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Samsung Galaxy S24 Ultra",
                                    Description = "Екран (6.8\", Dynamic AMOLED 2X, 3120x1440) / Qualcomm Snapdragon 8 Gen 3 for Galaxy (3.4 ГГц + 3.15 ГГц + 2.96 ГГц + 2.27 ГГц) / основна квадрокамера: 200 Мп + 50 Мп + 12 Мп + 10 Мп, фронтальна 12 Мп / RAM 12 ГБ / 1 ТБ вбудованої пам'яті / 3G / LTE / 5G / GPS / підтримка 2х SIM-карток (Nano-SIM) / Android 14 / 5000 мА * год",
                                    Price = 71999m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/398085804.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/398085805.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/398085806.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/398085807.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/398085808.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Motorola G54 Power",
                                    Description = "Екран (6.5\", IPS, 2400x1080) / MediaTek Dimensity 7020 (2.2 ГГц + 2.0 ГГц) / подвійна основна камера: 50 Мп + 8 Мп, фронтальна камера: 16 Мп / RAM 12 ГБ / 256 ГБ вбудованої памʼяті + microSD (до 1 ТБ) / 3G / LTE / 5G / GPS / Nano-SIM + eSIM / Android 13 / 6000 мА*год",
                                    Price = 8499m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/366846770.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/366846771.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/366846772.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/366846773.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/366846774.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = SamsungGalaxyA34,
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "ivanenko.oleksandr@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Покажи свій потенціал з Lenovo IdeaPad Gaming 3 15ACH6! 🎮💥",
                                Content = "🔥 Вражаюча продуктивність: Побудований на базі потужного процесора AMD Ryzen 5 5500H з максимальною швидкістю до 4.2 ГГц, цей ноутбук готовий взяти на себе будь-яке завдання без затримок.\r\n\r\n🔝 Поглиблене іммерсивне досвід: Очищення до деталей завдяки дисплею IPS Full HD з частотою оновлення 144 Гц. Кожен момент гри стає більш реалістичним та захопливим.\r\n\r\n💨 Швидкість без компромісів: Оперативна пам'ять об'ємом 16 ГБ та SSD з об'ємом 512 ГБ забезпечать миттєвий доступ до улюблених ігор та програм.\r\n\r\n🚀 Графіка нового покоління: Насолоджуйтесь бездоганними графічними можливостями завдяки відеокарті NVIDIA GeForce RTX 2050 з 4 ГБ пам'яті. Готуйтеся до вражаючих візуальних ефектів та реалістичних деталей.\r\n\r\n🎙️ Підготовка до спілкування: Вбудована веб-камера та функції LAN, Wi-Fi та Bluetooth забезпечать безперешкодну комунікацію та спільну гру з друзями та командою.\r\n\r\n👾 Готовий до бою: З ноутбуком Lenovo IdeaPad Gaming 3 15ACH6 ти завжди будеш готовий до наступного виклику. Лише ти та твої улюблені ігри, без обмежень та перешкод. Стань найкращим у світі геймерів з Lenovo!",
                                image = "Lenovo IdeaPad Gaming 3 15ACH6.webp",
                            },
                            new BlogPost
                            {
                                Author = "ivanenko.oleksandr@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Захопи світ разом з Apple iPhone 15 Pro Max! 📱✨",
                                Content = "🌟 Поглиблений екран: Живи кожним кадром на вражаючому OLED Super Retina XDR дисплеї розміром 6.7\" з неймовірною роздільною здатністю 2796x1290. Кожен дотик — нова порція задоволення.\r\n\r\n🚀 Потужний двигун: Запускай додатки миттєво та виконуй завдання швидше за будь-коли завдяки процесору Apple A17 Pro. Почуй потужність на власному досвіді!\r\n\r\n📸 Очікуй на дива: Запечатлюй найяскравіші моменти свого життя з основною квадро-камерою у складі 48 Мп + 12 Мп + 12 Мп + 12 Мп та фронтальною камерою на 12 Мп. Кожен знімок — шедевр від Apple.\r\n\r\n📱 Необмежені можливості: Зберігай свої найцінніші моменти без обмежень завдяки вбудованій пам'яті на 256 ГБ. Всі твої додатки, фотографії та відео завжди під рукою.\r\n\r\n📶 Підключення до майбутнього: Вперед до швидкісного майбутнього з підтримкою 3G, LTE та 5G. Будь завжди на зв'язку та готовий до нових викликів.\r\n\r\n📍 Навігація без меж: Не загубишся ніде завдяки GPS. Впевнено вперед до нових вражень та пригод з iPhone 15 Pro Max!\r\n\r\n📱 Працюй як професіонал: Використовуй iOS 17 — найновішу операційну систему від Apple — і насолоджуйся новими функціями та можливостями!\r\n\r\nЗахопи світ у своїх руках з Apple iPhone 15 Pro Max! 🚀📱",
                                image = "Apple iPhone 15 Pro Max.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = CortC4Deluxe.Price,
                                Date = DateTime.Now,
                                DeliveryAddress = "Вулиця Лісова, 10, Київ",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = CortC4Deluxe.Id,
                                        Products = CortC4Deluxe,
                                        Quantity = 1,
                                        Status = 1,
                                    }
                                }
                            },
                            new Orders
                            {
                                TotalPrice = AppleIPhoneEarPods.Price,
                                Date = DateTime.Now.AddDays(-2),
                                DeliveryAddress = "Вулиця Лісова, 10, Київ",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = AppleIPhoneEarPods.Id,
                                        Products = AppleIPhoneEarPods,
                                        Quantity = 1,
                                        Status = 3,
                                    }
                                }
                            }
                        }
                    };

                    await context.AddAsync(user1);

                    var LenovoIdeaPad3 = new Products
                    {
                        Name = "Ноутбук Lenovo IdeaPad 3 15IAU7",
                        Description = "Экран 15.6\" IPS (1920x1080) Full HD, матовый / Intel Core i5-1235U (0.9 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / Intel Iris Xe Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / без ОС / 1.63 кг / серый",
                        Price = 22999m,
                        Categories = laptops,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/374343935.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/374343936.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/374343937.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/374343938.jpg"
                            },
                        }
                    };
                    var AppleIPhone15 = new Products
                    {
                        Name = "Мобільний телефон Apple iPhone 15 Pro",
                        Description = "Екран (6.1\", OLED (Super Retina XDR), 2556x1179) / Apple A17 Pro / основна квадро-камера: 48 Мп + 12 Мп + 12 Мп + 12 Мп, фронтальна камера: 12 Мп / 128 ГБ вбудованої пам'яті / 3G / LTE / 5G / GPS / Nano-SIM / iOS 17",
                        Price = 49999m,
                        Categories = mobilePhones,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/364824496.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/364824495.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/364824497.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/364824498.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/364824499.jpg"
                            },
                        },
                    };

                    await context.AddRangeAsync(LenovoIdeaPad3, AppleIPhone15);

                    var user2 = new Users
                    {
                        Email = "anna.kovalchuk@example.com",
                        Password = PasswordManager.HashPassword("11AAanna"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380955555555",
                        Surname = "Ковальчук",
                        Name = "Анна",
                        Patronimic = "Ігорівна",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = LenovoIdeaPad3,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук ASUS VivoBook 17X M3704YA-AU091",
                                    Description = "Екран 17.3\" IPS (1920x1080) Full HD, матовий / AMD Ryzen 5 7530U (2.0 - 4.5 ГГц) / RAM 16 ГБ / SSD 1 ТБ / AMD Radeon Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.1 кг / чорний",
                                    Price = 24499m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/386671380.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/386671382.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/386671468.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/386671471.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук Acer Aspire 5 A515-57G-35VM",
                                    Description = "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i3-1215U (3.3 - 4.4 ГГц) / RAM 8 ГБ / SSD 512 ГБ / nVidia GeForce RTX 2050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 1.76 кг / сірий",
                                    Price = 23999m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/395970280.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/398662376.png"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/395970281.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/395970282.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук HP Victus Gaming Laptop 15-fa0020ua",
                                    Description = "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / Intel Core i5-12450H (2.0 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / DOS / 2.29 кг / сірий",
                                    Price = 32999m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/368225648.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/368225649.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/368225650.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/368225651.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ноутбук Lenovo IdeaPad Gaming 3 15ARH7",
                                    Description = "Екран 15.6\" IPS (1920x1080) Full HD 120 Гц, матовий / AMD Ryzen 5 6600H (3.3 - 4.5 ГГц) / RAM 16 ГБ / SSD 1 ТБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.3 кг / темно-сірий",
                                    Price = 34999m,
                                    Categories = laptops,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/393629509.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/398696875.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/393629510.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/393629511.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = AppleIPhone15,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Samsung Galaxy S23 Ultra",
                                    Description = "Екран (6.8\", Dynamic AMOLED 2X, 3088x1440) / Qualcomm Snapdragon 8 Gen 2 for Galaxy (3.36 ГГц + 2.8 ГГц + 2.8 ГГц + 2.0 ГГц) / основна квадро-камера: 200 Мп + 12 Мп + 10 Мп + 10 Мп, фронтальна 12 Мп / RAM 12 ГБ / 512 ГБ вбудованої памʼяті / 3G / LTE / 5G / GPS / підтримка 2х SIM-карт (Nano-SIM) / Android 13 / 5000 мА*год",
                                    Price = 55999m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/310649358.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/310649357.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/310649359.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/310649360.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/310649361.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Samsung Galaxy S23",
                                    Description = "Екран (6.1\", Dynamic AMOLED 2X, 2340x1080) / Qualcomm Snapdragon 8 Gen 2 for Galaxy (3.36 ГГц + 2.8 ГГц + 2.8 ГГц + 2.0 ГГц) / потрійна основна камера: 50 Мп + 12 Мп + 10 Мп, фронтальна 12 Мп / RAM 8 ГБ / 128 ГБ вбудованої памʼяті / 3G / LTE / 5G / GPS / підтримка 2х SIM-карт (Nano-SIM) / Android 13 / 3900 мА*год",
                                    Price = 33999m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/310594229.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/310594230.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/310594231.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/310594232.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/310594239.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Motorola",
                                    Description = "Екран (6.5\", LCD, 2400x1080) / Qualcomm Snapdragon 680 (2.4 ГГц) / основна потрійна камера: 50 Мп + 8 Мп + 2 Мп, фронтальна камера: 16 Мп / RAM 8 ГБ / 256 ГБ вбудованої пам’яті + microSD (до 1 ТБ) / 3G / LTE / GPS / підтримка 2х SIM-карт (Nano-SIM) / Android 12 / 5000 мА*ч",
                                    Price = 5999m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/350974343.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/350974345.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/350974346.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/350974347.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/350974348.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мобільний телефон Apple iPhone 13",
                                    Description = "Екран (6.1\", OLED (Super Retina XDR), 2532x1170) / Apple A15 Bionic / подвійна основна камера: 12 Мп + 12 Мп, фронтальна камера: 12 Мп / 128 ГБ вбудованої пам'яті / 3G / LTE / 5G / GPS / Nano-SIM, eSIM / iOS 15",
                                    Price = 26999m,
                                    Categories = mobilePhones,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/221214139.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/221026603.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/221214140.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/221214141.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/221214142.jpg"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "anna.kovalchuk@example.com",
                                CreatedAt = DateTime.Now.AddDays(-1),
                                Title = "Освіж свій день з Acer Aspire 5 A515-57G-35VM! 💻✨",
                                Content= "🔥 Вражаючий дизайн: Зустрічай новий рівень стилю з ноутбуком Acer Aspire 5 A515-57G-35VM у сірому кольорі. Елегантний та стильний, він привертає увагу та вражає своєю легкістю.\r\n\r\n💡 Продуктивність на висоті: Завдяки потужному процесору Intel Core i3-1215U з частотою до 4.4 ГГц, цей ноутбук з легкістю впорається з будь-яким завданням, будь то робота чи розваги.\r\n\r\n💻 Безкомпромісна швидкість: Отримай бездоганний досвід роботи завдяки SSD об'ємом 512 ГБ та оперативної пам'яті на 8 ГБ. Забудь про затримки та просто насолоджуйся роботою.\r\n\r\n🎮 Графіка відмінного рівня: Насолоджуйся графічними можливостями ноутбука завдяки відеокарті NVIDIA GeForce RTX 2050 з 4 ГБ пам'яті. Легко відтворюй свої улюблені ігри з найкращими налаштуваннями графіки.\r\n\r\n📶 Завжди на зв'язку: Будь у курсі подій завдяки вбудованим модулям LAN, Wi-Fi та Bluetooth. Завжди на зв'язку з друзями та колегами, незалежно від місця знаходження.\r\n\r\n📸 Запечатлюй моменти: Вбудована веб-камера допоможе тобі зберегти найяскравіші моменти свого життя. Ділитися враженнями та спогадами стало ще легше.\r\n\r\n🚀 Готовий до творчості: Acer Aspire 5 A515-57G-35VM — твій надійний помічник у всіх справах. Робота, навчання, розваги — все в одному ноутбуці!\r\n\r\nОсвіж свій день та досягай нових висот з Acer Aspire 5 A515-57G-35VM! 💼🚀",
                                image = "Acer Aspire 5 A515-57G-35VM.webp",
                            },
                            new BlogPost
                            {
                                Author = "anna.kovalchuk@example.com",
                                CreatedAt = DateTime.Now.AddDays(-1),
                                Title = "Зустрічай новий Samsung Galaxy S23 Ultra - безкомпромісний в світі технологій! 🚀📱",
                                Content = "🌌 Відкрий для себе безмежний світ на екрані Dynamic AMOLED 2X розміром 6.8\" з роздільною здатністю 3088x1440. Живи кожним пікселем з яскравими кольорами та неймовірною деталізацією.\r\n\r\n🚀 Майбутнє у твоїх руках: Проведи кожен крок з новим процесором Qualcomm Snapdragon 8 Gen 2 for Galaxy, що працює на чотирьох ядрах зі швидкістю до 3.36 ГГц. Відчуй максимальну продуктивність в кожній дії.\r\n\r\n📸 Вражаюча камера: Запечатлюй кожен момент з основною квадро-камерою на 200 Мп + 12 Мп + 10 Мп + 10 Мп, що доповнюється потужною фронтальною камерою на 12 Мп. Твої фото та відео будуть завжди бездоганними.\r\n\r\n💡 Неймовірна продуктивність: Велика оперативна пам'ять на 12 ГБ та ще більший обсяг вбудованої пам'яті на 512 ГБ забезпечать швидкий доступ до всіх твоїх даних та додатків.\r\n\r\n📶 Підключення до майбутнього: Насолоджуйся швидкістю 5G та завжди будь на зв'язку завдяки підтримці 3G та LTE. Де б ти не був, твій Samsung Galaxy S23 Ultra завжди буде на зв'язку.\r\n\r\n📍 Нові горизонти з GPS: Завжди знаходь свій шлях з надійною підтримкою GPS в твоєму смартфоні.\r\n\r\n📱 Працюй на Android 13: Нова версія операційної системи від Google готова до найсміливіших ідей та проектів.\r\n\r\nЗавдяки максимальній потужності, неймовірній камері та швидкісному підключенню, Samsung Galaxy S23 Ultra — твій вірний супутник у світі інновацій! 💫📸",
                                image = "Samsung Galaxy S23 Ultra.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = HPPavilion.Price + SamsungGalaxyA34.Price,
                                Date = DateTime.Now.AddDays(-3),
                                DeliveryAddress = "Вулиця Сонячна, 24А, Львів",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = HPPavilion.Id,
                                        Products = HPPavilion,
                                        Quantity = 1,
                                        Status = 2,
                                    },
                                    new OrdersProducts
                                    {
                                        ProductId = SamsungGalaxyA34.Id,
                                        Products = SamsungGalaxyA34,
                                        Quantity = 1,
                                        Status = 5,
                                    }
                                }
                            },
                        }
                    };

                    await context.AddAsync(user2);

                    var BOSCHWAN28263UA = new Products
                    {
                        Name = "Пральна машина повногабаритна BOSCH WAN28263UA",
                        Description = "Максимальне завантаження білизни: 8 кг\r\nТип двигуна: Інверторний\r\nКлас енергоспоживання: А+++\r\nТехнічні особливості: З дисплеєм,з можливістю дозавантаження білизни\r\nГабарити (ВхШхГ), см: 84.8 х 59.8 х 55\r\nМаксимальна швидкість віджимання, об/хв: 1400",
                        Price = 21499m,
                        Categories = washingMachines,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/325139122.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/325139123.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/325139124.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/325139125.jpg"
                            },
                        }
                    };
                    var AndaSeatKaiser = new Products
                    {
                        Name = "Крісло для геймерів Anda Seat Kaiser Frontier",
                        Description = "Розміри сидіння: 58 x 54 см",
                        Price = 13999m,
                        Categories = armchairs,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/412750785.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/412750786.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/412750787.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/412750788.jpg"
                            },
                        }
                    };

                    await context.AddRangeAsync(BOSCHWAN28263UA, AndaSeatKaiser);

                    var user3 = new Users
                    {
                        Email = "iryna.petrenko@example.com",
                        Password = PasswordManager.HashPassword("11AAiryna"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380933333333",
                        Surname = "Петренко",
                        Name = "Ірина",
                        Patronimic = "Олексіївна",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = BOSCHWAN28263UA,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Пральна машина вузька WHIRLPOOL WRBSB 6228 B UA",
                                    Description = "Максимальне завантаження білизни: 6 кг\r\nТип двигуна: Інверторний\r\nКількість програм: 16\r\nКлас енергоспоживання: А+++\r\nТехнічні особливості: З дисплеєм\r\nГабарити (ВхШхГ), см: 85 х 59.5 х 42.5\r\nМаксимальна швидкість віджимання, об/хв: 1200",
                                    Price = 13333m,
                                    Categories = washingMachines,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/286602036.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/286602037.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/286602038.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/286602039.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Пральна машина вузька SAMSUNG WW62J32G0PW/UA",
                                    Description = "Максимальне завантаження білизни: 6 кг\r\nТип двигуна: Інверторний\r\nКлас енергоспоживання: А+++\r\nТехнічні особливості:З дисплеєм, з парою\r\nГабарити (ВхШхГ), см: 85 x 60.1 x 45\r\nМаксимальна швидкість віджимання, об/хв: 1200",
                                    Price = 14999m,
                                    Categories = washingMachines,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/275783559.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/315604658.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/275783560.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/275783561.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Пральна машина вузька BEKO WUE 6626 XBCW",
                                    Description = "Максимальне завантаження білизни: 6 кг\r\nТип двигуна: Інверторний\r\nКількість програм: 15\r\nКлас енергоспоживання: А+++\r\nТехнічні особливості: З можливістю дозавантаження білизни, з парою\r\nГабарити (ВхШхГ), см: 84 х 60 х 44\r\nМаксимальна швидкість віджимання, об/хв: 1200",
                                    Price = 12999m,
                                    Categories = washingMachines,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/371621067.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/334711121.png"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/371621093.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/371621117.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ігрове крісло Anda Seat Kaiser",
                                    Description = "Розміри сидіння: 52.5 х 57 см",
                                    Price = 17999m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/360527000.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/360527001.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/360527002.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/360527003.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Крісло GT RACER B-4011",
                                    Description = "Матеріал оббивки: Сітка",
                                    Price = 1999m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/352972580.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/352972581.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/352972582.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Крісло для геймерів Hator Flash",
                                    Description = "Розміри сидіння: 48 х 49 см",
                                    Price = 4299m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/379941502.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/379941503.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/379941504.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/379941505.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = AndaSeatKaiser,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Крісло GT RACER B-4029",
                                    Description = "Розміри сидіння: 47х46 см",
                                    Price = 1559m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/255938654.png"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/255938655.png"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "iryna.petrenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-2),
                                Title = "Забудьте про проблеми з пранням завдяки BOSCH WAN28263UA! 🌀✨",
                                Content = "👚 Максимальний комфорт: Завантажуйте до 8 кг білизни і насолоджуйтеся чистотою та свіжістю кожного дня. Більше ніяких непотрібних перестановок!\r\n\r\n🔋 Економте енергію: З класом енергоспоживання А+++, ця пральна машина ефективно використовує енергію, зберігаючи ваші кошти та ресурси планети.\r\n\r\n💡 Інтелектуальні рішення: Оснащена інверторним двигуном та дисплеєм, BOSCH WAN28263UA пропонує найсучасніші технології для зручного та ефективного прання.\r\n\r\n🔄 Гнучкість у використанні: З можливістю дозавантаження білизни в будь-який час, ви можете легко додати забуті речі під час циклу прання.\r\n\r\n🌪️ Максимальна швидкість віджимання: Досягайте ідеально сухої білизни завдяки швидкості віджимання до 1400 об/хв. Готові до використання вже за миттєвість!\r\n\r\n📏 Компактні розміри: Завдяки габаритам 84.8 х 59.8 х 55 см, ця пральна машина ідеально впишеться в будь-який інтер'єр вашої кухні чи пральні.\r\n\r\nЗробіть ваше прання легким та ефективним з BOSCH WAN28263UA! 💧👕",
                                image = "BOSCH WAN28263UA.webp",
                            },
                            new BlogPost
                            {
                                Author = "iryna.petrenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-2),
                                Title = "Освоюйте світ гри з Кріслом для геймерів Anda Seat Kaiser Frontier! 🎮💺",
                                Content = "🚀 Зручність та ергономіка: Крісло Anda Seat Kaiser Frontier розроблене спеціально для геймерів, з урахуванням їхніх потреб та комфорту. Воно забезпечить вам правильну підтримку під час гри або роботи за комп'ютером, навіть під час тривалих сесій.\r\n\r\n🔥 Неперевершений дизайн: Крісло має стильний та сучасний вигляд, що додає вашому ігровому простору атмосферу професіоналізму та класу. Завдяки відмінній якості матеріалів та конструкції, воно служитиме вам протягом багатьох років.\r\n\r\n💪 Максимальна міцність: Anda Seat Kaiser Frontier відзначається своєю надзвичайною міцністю та надійністю. Воно може витримати великі навантаження та забезпечить вам стабільність під час інтенсивних рухів під час гри.\r\n\r\n🎨 Персоналізація: Виберіть крісло, яке відповідає вашому стилю та смаку, завдяки різноманітним варіантам кольорів та дизайну.\r\n\r\n🎯 Підвищте свою продуктивність: Завдяки правильному положенню тіла та комфортному сидінню, Anda Seat Kaiser Frontier допоможе вам сконцентруватися на грі та досягти нових висот у своїх ігрових досягненнях.\r\n\r\nПогрузіться у світ геймінгу з Anda Seat Kaiser Frontier та відчуйте всю енергію та емоції гри! 🌟 ",
                                image = "Anda Seat Kaiser Frontier.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = LenovoIdeaPad3.Price,
                                Date = DateTime.Now,
                                DeliveryAddress = "Проспект Незалежності, 55, Харків",
                                Delivery = novaposhta,
                                DeliveryId = novaposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = LenovoIdeaPad3.Id,
                                        Products = LenovoIdeaPad3,
                                        Quantity = 1,
                                        Status = 4,
                                    }
                                }
                            },
                            new Orders
                            {
                                TotalPrice = AppleIPhone15.Price,
                                Date = DateTime.Now.AddDays(-1),
                                DeliveryAddress = "Проспект Незалежності, 55, Харків",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = AppleIPhone15.Id,
                                        Products = AppleIPhone15,
                                        Quantity = 1,
                                        Status = 3,
                                    }
                                }
                            }
                        }
                    };

                    await context.AddAsync(user3);

                    var ELECTROLUXEW6S406WU = new Products
                    {
                        Name = "Пральна машина вузька ELECTROLUX EW6S406WU",
                        Description = "Максимальне завантаження білизни: 6 кг\r\nТип двигуна: Колекторний\r\nКількість програм: 15\r\nКлас енергоспоживання: А+++\r\nТехнічні особливості: З дисплеєм, з  парою\r\nГабарити (ВхШхГ), см: 85 х 60 х 38\r\nМаксимальна швидкість віджимання, об/хв: 1000",
                        Price = 11799m,
                        Categories = washingMachines,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/368176035.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/179852592.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/179852593.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/179852595.jpg"
                            },
                        }
                    };
                    var Special4YouRiko = new Products
                    {
                        Name = "Крісло Special4You Riko Black/grey",
                        Description = "Розміри сидіння: 48.5 х 50 см",
                        Price = 3999m,
                        Categories = armchairs,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/146412160.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/146412200.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/146412256.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/146412303.jpg"
                            },
                        }
                    };

                    await context.AddRangeAsync(ELECTROLUXEW6S406WU, Special4YouRiko);

                    var user4 = new Users
                    {
                        Email = "volodymyr.sydorenko@example.com",
                        Password = PasswordManager.HashPassword("11AAvolodymyr"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380922222222",
                        Surname = "Сидоренко",
                        Name = "Володимир",
                        Patronimic = "Миколайович",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Пральна машина вузька ELECTROLUX EW6S404WU",
                                    Description = "Максимальне завантаження білизни: 4 кг\r\nТип двигуна: Колекторний\r\nКількість програм: 14\r\nКлас енергоспоживання: А+\r\nТехнічні особливості: З дисплеєм, з парою\r\nГабарити (ВхШхГ), см: 85 x 59.5 x 37.2\r\nМаксимальна швидкість віджимання, об/хв: 1000",
                                    Price = 10999m,
                                    Categories = washingMachines,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/368175670.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/244748653.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/339010550.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/244748652.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Пральна машина вузька LG FH0J3NDN0",
                                    Description = "Максимальне завантаження білизни: 6 кг\r\nТип двигуна: Інверторний з прямим приводом\r\nКількість програм: 10\r\nКлас енергоспоживання: А+++\r\nТехнічні особливості: З дисплеєм, з розбірним баком\r\nГабарити (ВхШхГ), см: 85 х 60 х 44\r\nМаксимальна швидкість віджимання, об/хв: 1000",
                                    Price = 15499m,
                                    Categories = washingMachines,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/12579249.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/12579264.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/12579275.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/12579310.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = ELECTROLUXEW6S406WU,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Пральна машина з вертикальним завантаженням INDESIT BTW A51052",
                                    Description = "Максимальне завантаження білизни: 5 кг\r\nТип двигуна: Колекторний\r\nКількість програм: 10\r\nКлас енергоспоживання: А++\r\nТехнічні особливості: LED-індикація, з розбірним баком\r\nГабарити (ВхШхГ), см: 90 х 40 х 60\r\nМаксимальна швидкість віджимання, об/хв: 1000",
                                    Price = 11999m,
                                    Categories = washingMachines,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/385635700.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/413983076.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/413983083.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/16723661.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Пральна машина з сушкою LG F2V5GG9T",
                                    Description = "Максимальне завантаження білизни: 8.5 кг\r\nТип двигуна: Інверторний з прямим приводом\r\nКількість білизни під час сушіння: 5 кг\r\nКількість програм: 14\r\nКлас енергоспоживання: А\r\nТехнічні особливості: З дисплеєм, з парою, з розбірним баком\r\nГабарити (ВхШхГ), см: 85 x 60 x 47\r\nМаксимальна швидкість віджимання, об/хв: 1200",
                                    Price = 31499m,
                                    Categories = washingMachines,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/151302573.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/151302595.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/151302723.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/151302614.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Крісло Barsky Freelance Leather BFR-01",
                                    Description = "Розміри сидіння: 53 х 53 см",
                                    Price = 7990m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/149957025.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/149957088.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/149957261.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/149957302.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Крісло для геймерів Anda Seat Kaiser Frontier XL Grey Linen Fabric",
                                    Description = "Розміри сидіння: 58 x 54 см",
                                    Price = 13999m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/412750812.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/412750813.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/412750814.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/412750815.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = Special4YouRiko,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ігрове крісло Anda Seat Kaiser 3 Size L Grey Fabric",
                                    Description = "Розміри сидіння: 52.5 х 57 см",
                                    Price = 17999m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/360527013.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/360527014.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/360527015.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/360527017.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Крісло для геймерів HATOR Arc Fabric Stone Gray",
                                    Description = "Розміри сидіння: 44 x 52 см",
                                    Price = 15499m,
                                    Categories = armchairs,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/303448315.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/303448316.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/303448317.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/303448318.jpg"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "volodymyr.sydorenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-3),
                                Title = "Вигідно та зручно прання з пральною машиною ELECTROLUX EW6S404WU! 🌀✨",
                                Content = "👚 Компактна, але потужна: Незважаючи на свої компактні розміри, ця пральна машина може легко справлятися з завантаженням до 4 кг білизни. Ідеально підходить для невеликих пральних кімнат або кухонних просторів.\r\n\r\n🔋 Ефективне використання енергії: З класом енергоспоживання А+, ELECTROLUX EW6S404WU економно використовує електроенергію, допомагаючи зменшити ваші витрати на комунальні послуги.\r\n\r\n💡 Розмаїття програм: Оберіть ідеальний режим прання з 14 доступних програм, що відповідають різним типам тканин та ступеню забруднення білизни.\r\n\r\n🌬️ Забезпечте своїй білизні свіжість: Технологія з парою допоможе звільнити вашу білизну від неприємних запахів та забруднень, залишаючи її чистою та свіжою.\r\n\r\n📏 Практичні розміри: Завдяки компактним габаритам 85 x 59.5 x 37.2 см, ця пральна машина ідеально підходить для невеликих кухонних або пральних кімнат.\r\n\r\n💪 Надійність та довговічність: З колекторним двигуном та максимальною швидкістю віджимання до 1000 об/хв, ця пральна машина готова прослужити вам протягом багатьох років.\r\n\r\nЗабезпечте вашій білизні ідеальну чистоту та свіжість з ELECTROLUX EW6S404WU! 💧👕",
                                image = "ELECTROLUX EW6S404WU.webp",
                            },
                            new BlogPost
                            {
                                Author = "volodymyr.sydorenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-3),
                                Title = "Відчуй справжній комфорт під час гри з кріслом для геймерів HATOR Arc Fabric Stone Gray! 🎮💺",
                                Content = "🌟 Новий рівень затишку: Завдяки інноваційному дизайну та ергономічній формі, крісло HATOR Arc дарує неперевершений комфорт, дозволяючи тобі повністю зануритися у світ гри.\r\n\r\n💪 Міцність та надійність: Завдяки високоякісним матеріалам та міцній конструкції, крісло HATOR Arc витримає будь-які випробування, надаючи тобі впевненість у своїй підтримці.\r\n\r\n🔥 Стильний дизайн: Елегантний кольоровий відтінок Stone Gray надає кріслу HATOR Arc вишуканості та стилю, доповнюючи твій ігровий простір чарівним виглядом.\r\n\r\n🕹️ Гнучкі налаштування: Регульовані підлокітники, висота сидіння та нахил спинки дозволяють зручно налаштувати крісло під свої індивідуальні потреби та уподобання.\r\n\r\n👍 Підтримка та комфорт: Забезпечуй своїй спині оптимальну підтримку та розслабленість під час годин гри чи роботи за комп'ютером.\r\n\r\n🚀 Готовий до викликів: HATOR Arc Fabric Stone Gray - твій надійний супутник у світі геймінгу, готовий допомогти тобі завоювати нові вершини і досягати найкращих результатів.\r\n\r\nОтримай істинну задоволеність від гри з кріслом HATOR Arc Fabric Stone Gray! 💫🎮",
                                image = "HATOR Arc Fabric Stone Gray.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = BOSCHWAN28263UA.Price,
                                Date = DateTime.Now.AddDays(-5),
                                DeliveryAddress = "Вулиця Перемоги, 72, Одеса",
                                Delivery = novaposhta,
                                DeliveryId = novaposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = BOSCHWAN28263UA.Id,
                                        Products = BOSCHWAN28263UA,
                                        Quantity = 1,
                                        Status = 7,
                                    }
                                }
                            },
                            new Orders
                            {
                                TotalPrice = AndaSeatKaiser.Price,
                                Date = DateTime.Now.AddDays(-1),
                                DeliveryAddress = "Вулиця Перемоги, 72, Одеса",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = AndaSeatKaiser.Id,
                                        Products = AndaSeatKaiser,
                                        Quantity = 1,
                                        Status = 1,
                                    }
                                }
                            }
                        }
                    };

                    await context.AddAsync(user4);

                    var MilwaukeeM12 = new Products
                    {
                        Name = "Акумуляторна дриль Milwaukee M12 BDD-202C",
                        Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний",
                        Price = 7901m,
                        Categories = screws,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/229527105.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/229527106.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/229527107.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/229527108.jpg"
                            },
                        }
                    };
                    var KRONERDerbyKRP = new Products
                    {
                        Name = "Кухонна мийка KRONER Derby KRP Brush 5050HM",
                        Description = "Матеріал: Нержавіюча сталь\r\nТип встановлення: Під стільницю, Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 50 х 50 х 21.5 см\r\nСифон: Є\r\nКолір: Сірий",
                        Price = 2129m,
                        Categories = kitchenSinks,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/414315228.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/414308211.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/414308213.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/414308214.jpg"
                            },
                        }
                    };

                    await context.AddRangeAsync(MilwaukeeM12, KRONERDerbyKRP);

                    var user5 = new Users
                    {
                        Email = "marina.grishchenko@example.com",
                        Password = PasswordManager.HashPassword("11AAmarina"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380911111111",
                        Surname = "Грищенко",
                        Name = "Марина",
                        Patronimic = "Вікторівна",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Дриль-шурупокрут акумуляторний RZTK RD 1220Li",
                                    Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Україна",
                                    Price = 1099m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/191580548.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/191580555.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/355040652.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/355040653.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Акумуляторний дриль-шурупокрут Bosch EasyDrill",
                                    Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Німеччина",
                                    Price = 3594m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/319765084.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/319765121.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/319765144.png"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/319765164.png"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = MilwaukeeM12,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Дриль-шурупокрут акумуляторний RZTK RD 1220Li",
                                    Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Україна",
                                    Price = 1999m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/191575882.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/281007180.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/191575909.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/355043786.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Дриль-шурупокрут акумуляторний RZTK RD 1213Li",
                                    Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Україна",
                                    Price = 749m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/33220071.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/345962704.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/345962705.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/408946167.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка ADAMANT Horizon чорна",
                                    Description = "Матеріал: Граніт\r\nТип встановлення: Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 78 х 49.5 х 23 см\r\nСифон: Є\r\nКолір: Чорний",
                                    Price = 4500m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/340995762.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/340995763.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/340995764.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/340995765.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка KRONER (KRP)",
                                    Description = "Матеріал: Нержавіюча сталь\r\nТип встановлення: Під стільницю\r\nГабарити (ШхГхВ)/Діаметр мийки: 48 х 43 х 21.5 см\r\nСифон: Є\r\nКолір: Хром",
                                    Price = 2358m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/279063290.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/380903967.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/279063294.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/279063296.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = KRONERDerbyKRP,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка ADAMANT Univer графiт",
                                    Description = "Матеріал: Граніт\r\nТип встановлення: Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 56 х 50 х 20 см\r\nСифон: Є\r\nКолір: Сірий",
                                    Price = 3750m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/354378541.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/354378542.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/354378543.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/354378544.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка Kroner Derby KRP Brush - 5050HM",
                                    Description = "Матеріал: Нержавіюча сталь\r\nТип встановлення: Під стільницю, Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 50 х 50 х 21.5 см\r\nСифон: Є\r\nКолір: Сірий",
                                    Price = 3750m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/354378541.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/354378542.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/354378543.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/354378544.jpg"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "marina.grishchenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-1),
                                Title = "Розбудіть свій потенціал з дриль-шурупокрутом аккумуляторним RZTK RD 1220Li! 🔋⚙️",
                                Content = "🛠️ Безкомпромісна потужність: Завдяки надійному акумулятору, цей інструмент забезпечить вам потужність та продуктивність для виконання будь-яких завдань у вашому домі або на будівництві.\r\n\r\n🔩 Універсальний інструмент: З режимом дрилювання та шуруповертення, ви зможете легко впоратися з будь-якими матеріалами та роботами, від збірки меблів до ремонту.\r\n\r\n🔄 Зручна та ергономічна конструкція: Легкий та зручний у використанні, дриль-шурупокрут RZTK RD 1220Li має ергономічний дизайн, який дозволить вам працювати комфортно навіть протягом тривалого часу.\r\n\r\n🔋 Надійна та тривала робота: Інтегрований акумулятор забезпечить довгу автономну роботу без необхідності частого заряджання, дозволяючи вам зосередитися на виконанні завдань.\r\n\r\n💡 Легкий у використанні: Завдяки простому управлінню та інтуїтивно зрозумілому інтерфейсу, ви зможете швидко освоїти цей інструмент та використовувати його з великим задоволенням.\r\n\r\nВперед до нових звершень з дрилем-шурупокрутом аккумуляторним RZTK RD 1220Li! 🔧✨",
                                image = "RZTK RD 1220Li.webp",
                            },
                            new BlogPost
                            {
                                Author = "marina.grishchenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-1),
                                Title = "Перетворіть миття посуду на задоволення з кухонною мийкою Kroner Derby KRP Brush - 5050HM! 🍽️✨",
                                Content = "🌊 Ефективне миття: Завдяки високоякісному матеріалу та інноваційній конструкції, мийка Kroner Derby забезпечить вам швидке та ефективне миття посуду, зберігаючи його чистим та сяючим.\r\n\r\n💧 Велика ємність: З розмірами 50x50 см, ця мийка ідеально підходить для миття різноманітної посуду, від маленьких тарілок до великих каструль та сковорідок.\r\n\r\n🔧 Практичність та зручність: Завдяки глибокому дизайну та додатковому аксесуару - щітці для миття, ви зможете легко та швидко впоратися з навіть найбруднішими посудом.\r\n\r\n🔝 Відмінна якість: Кухонна мийка Kroner Derby виготовлена з високоякісних матеріалів, що забезпечить її довговічність та надійність.\r\n\r\n🎨 Стильний дизайн: Чисті лінії та сучасний вигляд роблять цю мийку відмінним доповненням до будь-якого інтер'єру кухні.\r\n\r\nНехай миття посуду стане приємною частиною вашого дня з кухонною мийкою Kroner Derby KRP Brush - 5050HM! 🚿✨",
                                image = "Kroner Derby KRP Brush - 5050HM.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = ELECTROLUXEW6S406WU.Price + Special4YouRiko.Price,
                                Date = DateTime.Now.AddDays(-10),
                                DeliveryAddress = "Вулиця Шевченка, 30, Дніпро",
                                Delivery = novaposhta,
                                DeliveryId = novaposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = ELECTROLUXEW6S406WU.Id,
                                        Products = ELECTROLUXEW6S406WU,
                                        Quantity = 1,
                                        Status = 4,
                                    },
                                    new OrdersProducts
                                    {
                                        ProductId = Special4YouRiko.Id,
                                        Products = Special4YouRiko,
                                        Quantity = 1,
                                        Status = 4,
                                    }
                                }
                            },
                        }
                    };

                    await context.AddAsync(user5);

                    var BoschProfessional = new Products
                    {
                        Name = "Безщітковий ударний акумуляторний дриль-шуруповерт Bosch Professional GSB 185 -li",
                        Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Німеччина",
                        Price = 6582m,
                        Categories = screws,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/328069955.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/328069957.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/328069958.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/328069959.jpg"
                            },
                        }
                    };
                    var ADAMANTHorizon = new Products
                    {
                        Name = "Кухонна мийка ADAMANT Horizon сахара",
                        Description = "Матеріал: Граніт\r\nТип встановлення: Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 78 х 49.5 х 23 см\r\nСифон: Є\r\nКолір: Бежевий",
                        Price = 4450m,
                        Categories = kitchenSinks,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/354877353.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/354877352.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/354343090.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/354877351.jpg"
                            },
                        }
                    };

                    await context.AddRangeAsync(BoschProfessional, ADAMANTHorizon);

                    var user6 = new Users
                    {
                        Email = "oleg.lysenko@example.com",
                        Password = PasswordManager.HashPassword("11AAoleg"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380944444444",
                        Surname = "Лисенко",
                        Name = "Олег",
                        Patronimic = "Валентинович",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Дриль-шурупокрут акумуляторний JCB 2.4 A, 45 Нм",
                                    Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Україна",
                                    Price = 5624m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/412417787.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/412417789.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/412417790.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/412417788.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Акумуляторний дриль-шурупокрут Bosch GSR 120-LI",
                                    Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Німеччина",
                                    Price = 3899m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/318015213.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/318015216.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/318015217.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/318015218.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = BoschProfessional,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Мережевий шуруповерт DWT BM-280 T",
                                    Description = "Джерело живлення: Мережа\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Швейцарія",
                                    Price = 1099m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/10652337.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Безщітковий акумуляторний дриль-шуруповерт Bosch Professional GSR 185-LI",
                                    Description = "Джерело живлення: Акумулятор\r\nТип патрона: Швидкозатискний\r\nКраїна реєстрації бренду: Німеччина",
                                    Price = 5479m,
                                    Categories = screws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/328075563.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/328075565.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/328075566.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/328075567.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка граніт ADAMANT Sun Сахара",
                                    Description = "Матеріал: Граніт\r\nТип встановлення: Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 51 х 51 х 20 см\r\nСифон: Є\r\nКолір: Бежевий",
                                    Price = 3300m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/357520315.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/357520316.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/357520317.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/357520318.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = ADAMANTHorizon,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка ADAMANT Minimal сiра",
                                    Description = "Матеріал: Граніт\r\nТип встановлення: Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 61.5 х 49.5 х 20 см\r\nСифон: Є\r\nКолір: Сірий",
                                    Price = 3900m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/340998909.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/340998928.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/340998929.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/340998930.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка граніт GRANADO Cadiz Black Shine GR2301",
                                    Description = "Матеріал: Граніт\r\nТип встановлення: Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 41 х 50 х 22 см\r\nСифон: Є\r\nКолір: Чорний",
                                    Price = 3999m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/407070056.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/407070057.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/407070058.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/407070059.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Кухонна мийка ADAMANT Univer Old-Stone",
                                    Description = "Матеріал: Граніт\r\nТип встановлення: Урізна\r\nГабарити (ШхГхВ)/Діаметр мийки: 56 х 50 х 20 см\r\nСифон: Є\r\nКолір: Білий",
                                    Price = 3900m,
                                    Categories = kitchenSinks,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/354445218.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/354445235.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/354445268.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/354445306.jpg"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "oleg.lysenko@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Зробіть свої ремонтні та будівельні проекти легкими та швидкими з аккумуляторним дриль-шурупокрутом JCB 2.4 A! 🔩⚡",
                                Content = "✅ Потужний та надійний: Завдяки 45 Нм крутного моменту, цей дриль-шурупокрут з легкістю впорається з будь-якою роботою, будь то свердління отворів чи завинчування шурупів.\r\n\r\n🔋 Велика продуктивність: Інтегрований акумулятор ємністю 2.4 A годин дозволяє вам працювати без перерви та зручно переміщатися по місцю роботи без обмежень кабелем.\r\n\r\n🛠️ Універсальність: Цей інструмент ідеально підходить для виконання різних завдань вдома чи на будівництві, забезпечуючи вам широкі можливості в роботі.\r\n\r\n🔧 Зручне використання: Легка та компактна конструкція дриля-шурупокрута JCB дозволяє вам працювати з комфортом та ефективністю, навіть в умовах обмеженого простору.\r\n\r\n💡 Проста в управлінні: Інтуїтивно зрозумілий інтерфейс та зручне розташування кнопок забезпечують вам легкість управління інструментом навіть для початківців.\r\n\r\nЗдійсніть свої будівельні та ремонтні мрії з аккумуляторним дрилем-шурупокрутом JCB 2.4 A! 💪🏗️",
                                image = "JCB 2.4 A.webp",
                            },
                            new BlogPost
                            {
                                Author = "oleg.lysenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-1),
                                Title = "Додайте характеру до своєї кухні з кухонною мийкою ADAMANT Univer Old-Stone! 🍽️✨",
                                Content = "🏛️ Класичний стиль: З виглядом, як старовинний камінь, ця кухонна мийка додасть вашій кухні особливого шарму та стилю. Кожен детально пророблений елемент створює відчуття традиції та елегантності.\r\n\r\n🌊 Практичність та функціональність: Завдяки глибокому боці та просторій частині для відведення води, мийка ADAMANT Univer Old-Stone забезпечить вам зручність та ефективність під час миття посуду.\r\n\r\n💧 Висока якість: Виготовлена з міцного та високоякісного матеріалу, ця мийка є надійним рішенням для вашої кухні, яке прослужить вам протягом багатьох років.\r\n\r\n🔝 Легкість у встановленні: Із зручною конструкцією для монтажу, ви зможете швидко та легко встановити цю мийку в вашій кухні, додаючи їй неповторний шарм.\r\n\r\n🎨 Виберіть стиль, що підходить вашому інтер'єру: ADAMANT Univer Old-Stone доступна в різних кольорах та фінішах, що дозволяє вам підібрати той, який найкраще впишеться в ваш дизайн кухні.\r\n\r\nЗробіть свою кухню місцем, де править гармонія та краса, з кухонною мийкою ADAMANT Univer Old-Stone! 💫🚰",
                                image = "ADAMANT Univer Old-Stone.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = MilwaukeeM12.Price,
                                Date = DateTime.Now.AddDays(-3),
                                DeliveryAddress = "Вулиця Садова, 14, Івано-Франківськ",
                                Delivery = novaposhta,
                                DeliveryId = novaposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = MilwaukeeM12.Id,
                                        Products = MilwaukeeM12,
                                        Quantity = 1,
                                        Status = 4,
                                    }
                                }
                            },
                            new Orders
                            {
                                TotalPrice = KRONERDerbyKRP.Price,
                                Date = DateTime.Now.AddDays(-1),
                                DeliveryAddress = "Вулиця Садова, 14, Івано-Франківськ",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = KRONERDerbyKRP.Id,
                                        Products = KRONERDerbyKRP,
                                        Quantity = 1,
                                        Status = 4,
                                    }
                                }
                            }
                        }
                    };

                    await context.AddAsync(user6);

                    var Supretto = new Products
                    {
                        Name = "Акумуляторна мініпила Supretto",
                        Description = "Країна реєстрації бренду: Україна",
                        Price = 1349m,
                        Categories = chainSaws,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/314191384.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/314191385.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/314191386.jpg"
                            },
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/314191387.jpg"
                            },
                        }
                    };
                    var Titan24 = new Products
                    {
                        Name = "Велосипед Titan 24\" Drone 2023 Рама-11\"",
                        Description = "Розмір рами: 11\"\r\nКлас: Гірський, Хардтейл\r\nКількість швидкостей: 7",
                        Price = 6990m,
                        Categories = bicycles,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/318945179.jpg"
                            },
                        }
                    };

                    await context.AddRangeAsync(Supretto, Titan24);

                    var user7 = new Users
                    {
                        Email = "tatyana.shevchenko@example.com",
                        Password = PasswordManager.HashPassword("11AAtatyana"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380977777777",
                        Surname = "Шевченко",
                        Name = "Тетяна",
                        Patronimic = "Василівна",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ланцюгова пила Grunhelm GES17-35B 1.7 кВт",
                                    Description = "Країна реєстрації бренду: Україна",
                                    Price = 1579m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/13697342.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/13697335.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ланцюгова електрична пила RZTK CS 1900E",
                                    Description = "Країна реєстрації бренду: Україна",
                                    Price = 1999m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/32524683.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/32524166.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/32524178.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/32524225.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ланцюгова електрична пила RZTK CS 2700E",
                                    Description = "Країна реєстрації бренду: Україна",
                                    Price = 1999m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/32541297.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/32541120.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/32541130.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/32541166.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = Supretto,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ланцюгова бензинова пила RZTK CS 5300",
                                    Description = "Країна реєстрації бренду: Україна",
                                    Price = 2999m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/238286325.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/238286326.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/345962777.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/345962778.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед CORRADO Piemont DB 26\" 21\"",
                                    Description = "Розмір рами: 21\"\r\nКлас: Гірський, Хардтейл\r\nКількість швидкостей: 21",
                                    Price = 13104m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/144339014.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед Stolen Casino 20.25\" 20\"",
                                    Description = "Розмір рами: 20\"\r\nКлас: BMX\r\nКількість швидкостей: 1",
                                    Price = 14600m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/320141040.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = Titan24,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед Titan Cobra 26\" Рама 17\"",
                                    Description = "Розмір рами: 17\"\r\nКлас: Гірський, Хардтейл\r\nКількість швидкостей: 27",
                                    Price = 13000m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/321606906.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/321606907.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/321606908.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/321606910.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед Ardis 20 МТВ AL PEPPA 10\"",
                                    Description = "Розмір рами: 10\"\r\nКлас: Гірський, Хардтейл\r\nКількість швидкостей: 1",
                                    Price = 5999m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/303237824.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/303237825.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/321606908.jpg"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "tatyana.shevchenko@example.com",
                                CreatedAt = DateTime.Now.AddDays(-1),
                                Title = "Впорайтесь з будь-якою задачею влітку з ланцюговою пилою Grunhelm GES17-35B! 🌳⚙️",
                                Content = "🌲 Надійний помічник: З потужністю 1.7 кВт, ця ланцюгова пила легко справляється з великими деревами та гілками, допомагаючи вам швидко та ефективно обрізати дерева та виконати різні садові роботи.\r\n\r\n🌿 Зручне використання: Легка та зручна в роботі, Grunhelm GES17-35B має ергономічну конструкцію та зручну рукоятку, що дозволяє вам комфортно працювати протягом тривалого часу без втоми.\r\n\r\n🔋 Висока продуктивність: Завдяки своїй потужності та високій швидкості ланцюга, ця пила забезпечить вам швидке та ефективне виконання робіт без зайвих зусиль.\r\n\r\n🛠️ Міцність та надійність: Виготовлена з високоякісних матеріалів та маючи міцну конструкцію, Grunhelm GES17-35B є надійним інструментом, який прослужить вам протягом багатьох років.\r\n\r\n🌞 Готуйте ваш сад до літа з ланцюговою пилою Grunhelm GES17-35B та насолоджуйтеся красою навколишнього світу! 🌿🌞",
                                image = "GES17-35B 1.7.webp",
                            },
                            new BlogPost
                            {
                                Author = "tatyana.shevchenko@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Зробіть своє життя легшим та зручнішим з ланцюговою електричною пилою RZTK CS 2700E! 🌳⚡️",
                                Content = "🔌 Потужний та ефективний: Завдяки своїй потужності та високому обертовому моменту, ця пила впорається з будь-якою завданнями, від обрізки дерев до підготовки дров на зиму.\r\n\r\n💡 Легкий у використанні: З електричним живленням та простим управлінням, RZTK CS 2700E дозволить вам швидко та легко впоратися з різними садовими роботами без зайвих зусиль.\r\n\r\n🌿 Зручна та ергономічна конструкція: Зручна рукоятка та легка конструкція забезпечують вам комфорт та стабільність під час роботи навіть протягом тривалого часу.\r\n\r\n🛠️ Міцність та надійність: Виготовлена з високоякісних матеріалів та маючи міцну конструкцію, RZTK CS 2700E є надійним партнером для вашого саду чи подвір'я.\r\n\r\n🌲 Насолоджуйтеся роботою на свіжому повітрі з легкою та потужною ланцюговою електричною пилою RZTK CS 2700E! 🌳💪",
                                image = "RZTK CS 2700E.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = ADAMANTHorizon.Price,
                                Date = DateTime.Now.AddDays(-2),
                                DeliveryAddress = "Вулиця Гагаріна, 7, Запоріжжя",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = ADAMANTHorizon.Id,
                                        Products = ADAMANTHorizon,
                                        Quantity = 1,
                                        Status = 1,
                                    }
                                }
                            },
                            new Orders
                            {
                                TotalPrice = BoschProfessional.Price,
                                Date = DateTime.Now.AddDays(-2),
                                DeliveryAddress = "Вулиця Гагаріна, 7, Запоріжжя",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = BoschProfessional.Id,
                                        Products = BoschProfessional,
                                        Quantity = 1,
                                        Status = 0,
                                    }
                                }
                            }
                        }
                    };

                    await context.AddAsync(user7);

                    var Grunhelm = new Products
                    {
                        Name = "Ланцюгова електропила Grunhelm GES22-40B 2 кВт",
                        Description = "Країна реєстрації бренду: Україна",
                        Price = 1899m,
                        Categories = chainSaws,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/232969814.jpg"
                            },
                        }
                    };
                    var ArdisBlaze = new Products
                    {
                        Name = "Велосипед Ardis Blaze 29\" 19\"",
                        Description = "Розмір рами: 19\"\r\nКлас: Гірський\r\nКількість швидкостей: 21",
                        Price = 13473m,
                        Categories = bicycles,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/226873355.jpg"
                            },
                        }
                    };

                    var user8 = new Users
                    {
                        Email = "dmitro.bondarenko@example.com",
                        Password = PasswordManager.HashPassword("11AAdmitro"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380966666666",
                        Surname = "Бондаренко",
                        Name = "Дмитро",
                        Patronimic = "Олександрович",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Висоторіз електричний Sequoia SEPS0810",
                                    Description = "Країна реєстрації бренду: Україна",
                                    Price = 2999m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/254872420.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/254872422.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/254872423.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/254872424.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = Grunhelm,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ланцюгова пила AL-KO EKS 2400/40",
                                    Description = "Країна реєстрації бренду: Німеччина",
                                    Price = 5999m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/50918673.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/50918679.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/50918689.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ланцюгова пила акумуляторна ТехАС TAOE-S12",
                                    Description = "Країна реєстрації бренду: Україна",
                                    Price = 5414m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/365001341.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/365001349.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/365001359.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/365001370.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Ланцюгова пила Зеніт Профі ЦПЛ-406/2800",
                                    Description = "Країна реєстрації бренду: Китай",
                                    Price = 2959m,
                                    Categories = chainSaws,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/10611311.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/10611320.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/10611331.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/10611341.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед Ardis Blaze 27.5\" 19\"",
                                    Description = "Розмір рами: 19\"\r\nКлас: Гірський\r\nКількість швидкостей: 21",
                                    Price = 11999m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/242269609.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = ArdisBlaze,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед Ardis Tucan 27.5\" 18\"",
                                    Description = "Розмір рами: 18\"\r\nКлас: Гірський, Хардтейл\r\nКількість швидкостей: 24",
                                    Price = 16443m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/238454671.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед Ardis Shultz 27.5\" 17\"",
                                    Description = "Розмір рами: 17\"\r\nКлас: Гірський, Хардтейл\r\nКількість швидкостей: 21",
                                    Price = 10549m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/267565443.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Велосипед Crossride Skyline 24\" 13\"",
                                    Description = "Розмір рами: 13\"\r\nКлас: Міський\r\nКількість швидкостей: 21",
                                    Price = 6630m,
                                    Categories = bicycles,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/242371588.jpg"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "dmitro.bondarenko@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Досліджуйте нові горизонти з велосипедом Crossride Skyline 24\" 13\"! 🚲✨",
                                Content = "🌟 Відкрийте нові шляхи: Цей велосипед допоможе вам відчути справжню свободу та пригоди, дозволяючи вам проїхатися по місту чи зануритися в невідомі стежки.\r\n\r\n🚴‍♀️ Комфорт та надійність: Зручне кермо та м'яке сидіння забезпечать вам комфортну поїздку, а надійна конструкція забезпечить вам безпеку та стабільність на дорозі.\r\n\r\n🏞️ Відмінний вибір для молодих експлорерів: З розміром колеса 24\" та рамою 13\", цей велосипед ідеально підходить для підлітків та молодих любителів активного відпочинку.\r\n\r\n🔧 Легкий у використанні: Проста управління та надійна конструкція зроблять катання на велосипеді Crossride Skyline легким та приємним досвідом для кожного.\r\n\r\n🌳 Долучайтеся до нашої спільноти велосипедистів та рушайте у захопливі пригоди разом з Crossride Skyline 24\" 13\"! 🌳🌄",
                                image = "Crossride Skyline.webp",
                            },
                            new BlogPost
                            {
                                Author = "dmitro.bondarenko@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Відправтеся в незабутню подорож з велосипедом Ardis Shultz 27.5\" 17\"! 🚴‍♂️✨",
                                Content = "🌟 Досягніть нових вершин: Цей велосипед - ваш надійний компаньйон у будь-яких пригодах. Він готовий взяти вас на відмінні покатушки як по місту, так і по гірських стежках.\r\n\r\n🚴‍♀️ Комфорт та маневреність: Зручне сидіння та стійке кермо забезпечать вам комфортну їзду, а маневреність цього велосипеда дозволить вам легко керувати навіть на складних маршрутах.\r\n\r\n🏞️ Досвідчуйте природу: З розміром колеса 27.5\" та рамою 17\", Ardis Shultz - ідеальний вибір для любителів велопрогулянок по лісовим стежкам та гірським дорогам.\r\n\r\n🔧 Надійність та якість: Ardis Shultz відомий своєю міцністю та надійністю, що робить його відмінним вибором для будь-якого велосипедиста.\r\n\r\n🌄 Приєднуйтеся до нас та рушайте в захопливі пригоди разом з Ardis Shultz 27.5\" 17\"! 🌄🌳 ",
                                image = "Ardis Shultz.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = Supretto.Price,
                                Date = DateTime.Now.AddDays(-4),
                                DeliveryAddress = "Проспект Леніна, 101, Чернігів",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = Supretto.Id,
                                        Products = Supretto,
                                        Quantity = 1,
                                        Status = 6,
                                    }
                                }
                            },
                            new Orders
                            {
                                TotalPrice = Titan24.Price,
                                Date = DateTime.Now.AddDays(-9),
                                DeliveryAddress = "Проспект Леніна, 101, Чернігів",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = Titan24.Id,
                                        Products = Titan24,
                                        Quantity = 1,
                                        Status = 5,
                                    }
                                }
                            }
                        }
                    };

                    await context.AddAsync(user8);

                    var LIGHTWAVE = new Products
                    {
                        Name = "Бас-гітара LIGHTWAVE VL-5FL nat",
                        Description = "Кількість струн: 5",
                        Price = 110593m,
                        Categories = bassGuitars,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/286843187.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/286843199.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/286843212.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/286843223.jpg"
                            },
                        }
                    };
                    var HatorHellraizer = new Products
                    {
                        Name = "Навушники Hator Hellraizer PC Edition Black",
                        Description = "Тип навушників: Накладні\r\nДіапазон частот навушників: 15 Гц - 25 кГц\r\nВага: 260 г",
                        Price = 799m,
                        Categories = headphone,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://content1.rozetka.com.ua/goods/images/big/378171828.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/378171829.jpg"
                            },
                            new Images
                            {
                                Url = "https://content.rozetka.com.ua/goods/images/big/378171830.jpg"
                            },
                            new Images
                            {
                                Url = "https://content2.rozetka.com.ua/goods/images/big/378171831.jpg"
                            },
                        }
                    };

                    var user9 = new Users
                    {
                        Email = "oksana.melnyk@example.com",
                        Password = PasswordManager.HashPassword("11AAoksana"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380988888888",
                        Surname = "Мельник",
                        Name = "Оксана",
                        Patronimic = "Іванівна",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гітара Cort C5 Plus ZBMH",
                                    Description = "Кількість струн: 5",
                                    Price = 19228m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/76681370.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/76681378.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/76681384.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/76681413.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гітара Cort Artisan Space 5",
                                    Description = "Кількість струн: 5\r\nМензура: 34\"",
                                    Price = 29783m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/409649059.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/409649062.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/409649066.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/409649069.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = LIGHTWAVE,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гітара Cort A6 Plus FMMH",
                                    Description = "Кількість струн: 6\r\nМензура: 34\"",
                                    Price = 35008m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/405978430.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/405978432.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/405978433.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гітара CORT A4 Ultra Ash",
                                    Description = "Кількість ладів: 24",
                                    Price = 48070m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/3828445.png"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/3828447.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/3828449.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/3828450.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники RZTK MS300 Black",
                                    Description = "Тип навушників: Повнорозмірні\r\nДіапазон частот навушників: 20 Гц - 20 кГц\r\nВага: 208 г",
                                    Price = 1099m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/282300410.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/383262363.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/274394507.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/274394508.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники Apple AirPods Pro",
                                    Description = "Тип навушників: TWS (2 окремо)\r\nВага:\r\nAirPods (один навушник): 5.3 г, Футляр з підтримкою бездротового заряджання: 45.2 г",
                                    Price = 10499m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/365137069.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/365137070.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/365137071.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/365137072.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = HatorHellraizer,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники Defunc True Music TWS Black",
                                    Description = "Тип навушників: TWS (2 окремо)\r\nДіапазон частот навушників: 20 Гц - 20 кГц\r\nВага: 40 г",
                                    Price = 888m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/368634951.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/368634961.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/368634969.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/187002224.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники Samsung Galaxy Buds2 Pro White",
                                    Description = "Тип навушників: TWS (2 окремо)\r\nВага: Навушника, г: 5.5",
                                    Price = 5999m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/280047786.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/280047792.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/280047788.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/280047793.jpg"
                                        },
                                    }
                                },
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "oksana.melnyk@example.com",
                                CreatedAt = DateTime.Now.AddDays(-2),
                                Title = "Окунисьте в світ музики з бас-гітарою Cort C5 Plus ZBMH! 🎸🎶",
                                Content = "🎵 Сповнена звуків: Завдяки високоякісним матеріалам та передовій технології, бас-гітара Cort C5 Plus ZBMH дарує вам потужний та глибокий звук, який зачаровує кожну ноту.\r\n\r\n🔊 Вишуканий дизайн: З елегантними кривими та приголомшливим зовнішнім виглядом, ця бас-гітара не лише звучить, але і виглядає чудово, створюючи неповторний образ на сцені.\r\n\r\n🎸 Комфорт та ергономіка: Зручна форма корпусу та зручний гриф забезпечать вам комфорт під час гри, дозволяючи вам віддатися музиці на повну катушку.\r\n\r\n🎶 Відмінна грація: Завдяки своєму чудовому звучанню та високій якості виготовлення, бас-гітара Cort C5 Plus ZBMH дозволить вам виразно виражати свої музичні ідеї та творити неповторні мелодії.\r\n\r\n🌟 Зробіть перший крок до світу музики з бас-гітарою Cort C5 Plus ZBMH і відчуйте справжню магію звуку! 🎵✨",
                                image = "Cort C5 Plus ZBMH.webp",
                            },
                            new BlogPost
                            {
                                Author = "oksana.melnyk@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Погрузись в світ музики з бездоганним звуком та комфортом з Samsung Galaxy Buds2 Pro White! 🎶✨",
                                Content = "🔊 Високоякісний звук: Незрівняний звук, що доставляється завдяки передовій технології, дозволить тобі відчути кожну ноту та насолодитися кришталево чистим звучанням улюбленої музики.\r\n\r\n🎵 Бездоганний дизайн: Елегантний білий колір та стильний дизайн роблять Samsung Galaxy Buds2 Pro White не лише джерелом прекрасного звуку, але й модним аксесуаром для будь-якого образу.\r\n\r\n🎧 Комфортний досвід: Зручний дизайн та м'які амбушури гарантують максимальний комфорт протягом усього дня, навіть під час тривалих прослуховувань.\r\n\r\n📱 Підключайся та насолоджуйся: Легке підключення до твого смартфона Samsung Galaxy або будь-якого іншого пристрою з Bluetooth, щоб насолоджуватися улюбленою музикою в будь-який момент.\r\n\r\n🔋 Відмінний час роботи: Завдяки великій місткості батареї, ти зможеш насолоджуватися музикою протягом тривалого часу, не переймаючись про зарядку.\r\n\r\nОчейне якісне звучання та стильний дизайн - все це з Samsung Galaxy Buds2 Pro White! 🎧🌟",
                                image = "Samsung Galaxy Buds2 Pro White.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = Grunhelm.Price + ArdisBlaze.Price,
                                Date = DateTime.Now,
                                DeliveryAddress = "Вулиця Вокзальна, 3, Рівне",
                                Delivery = ukrposhta,
                                DeliveryId = ukrposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = Grunhelm.Id,
                                        Products = Grunhelm,
                                        Quantity = 1,
                                        Status = 5,
                                    },
                                    new OrdersProducts
                                    {
                                        ProductId = ArdisBlaze.Id,
                                        Products = ArdisBlaze,
                                        Quantity = 1,
                                        Status = 3,
                                    }
                                }
                            },
                        }
                    };

                    await context.AddAsync(user9);

                    var user10 = new Users
                    {
                        Email = "inna.kozlova@example.com",
                        Password = PasswordManager.HashPassword("11AAinna"),
                        RoleId = userRole.Id,
                        Roles = userRole,
                        PhoneNumber = "380999999999",
                        Surname = "Козлова",
                        Name = "Інна",
                        Patronimic = "Юріївна",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гітара Deviser L-B3-5 BK",
                                    Description = "Розмір: Повнорозмірні (4/4)\r\nКількість струн: 5",
                                    Price = 5999m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/250775074.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/250775086.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/250775101.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/250775116.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гитара Cort GB-Modern 4",
                                    Description = "Мензура: 34\"",
                                    Price = 42845m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/308232360.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/308232375.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/308232387.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/308232397.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гитара Cort GB-Modern 5",
                                    Description = "Мензура: 34\"",
                                    Price = 47025m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/308232272.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/308232286.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/308232301.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/308232308.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Бас-гітара CORT Action V Plus",
                                    Description = "Кількість струн: 5\r\nМензура: 34\"",
                                    Price = 10137m,
                                    Categories = bassGuitars,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/334772939.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/334772942.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/334772944.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/334772947.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = CortC4Deluxe,
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники RZTK Buds TWS Black",
                                    Description = "Тип навушників: TWS (2 окремо)\r\nДіапазон частот навушників: 20—20 000 Гц\r\nВага:\r\nНаушники: 5 г x 2\r\nКейс: 35.2 г",
                                    Price = 649m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/378870632.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/378986546.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/378986533.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/378541095.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники Samsung Galaxy Buds FE SM-R400 White",
                                    Description = "Тип навушників: TWS (2 окремо)\r\nВага:\r\nВага навушника: 5.6 г\r\nВага зарядного кейса: 40 г",
                                    Price = 3499m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/370023502.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/370023503.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/370023504.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/370023505.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники Apple AirPods",
                                    Description = "Тип навушників: TWS (2 окремо)\r\nВага:\r\nAirPods (кожний навушник): 4 г\r\nЗарядний футляр: 38 г",
                                    Price = 4999m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/14270995.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/14271004.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/14271018.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/14271024.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Навушники RZTK MS 100 Black",
                                    Description = "Тип навушників: Повнорозмірні\r\nДіапазон частот навушників: 20 - 20000 Гц\r\nВага: 250 г",
                                    Price = 699m,
                                    Categories = headphone,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/298369963.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/298369964.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/396940628.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/396940630.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = AppleIPhoneEarPods,
                            },
                        },
                        BlogPost = new List<BlogPost>
                        {
                            new BlogPost
                            {
                                Author = "oksana.melnyk@example.com",
                                CreatedAt = DateTime.Now.AddDays(-2),
                                Title = "Очікуйте справжнього розкішного звуку з бас-гітарою Deviser L-B3-5 BK! 🎸✨",
                                Content = "🎶 Новий рівень музичного досвіду: З вражаючою комбінацією стилю та потужності, ця бас-гітара від Deviser забезпечить вам неймовірний звук, який захопить вас з перших акордів.\r\n\r\n🔊 Потужне звучання: За допомогою своїх потужних звукознімачів та інноваційних технологій, Deviser L-B3-5 BK забезпечить вам глибокі баси та чіткі високі ноти, створюючи музичну гармонію на висоті.\r\n\r\n🎸 Комфорт та ергономіка: Зручна форма та легка вага цієї бас-гітари роблять грати на ній легким та приємним досвідом для кожного музиканта.\r\n\r\n🎵 Вражайте своїх слухачів: Незалежно від того, чи ви новачок у світі музики або досвідчений виконавець, з бас-гітарою Deviser L-B3-5 BK ви завжди будете вражати своїх слухачів своїм музичним талантом.\r\n\r\nЗануртеся у світ музики з бас-гітарою Deviser L-B3-5 BK та розкрийте свій потенціал як музикант! 🎶🎸",
                                image = "Deviser L-B3-5 BK.webp",
                            },
                            new BlogPost
                            {
                                Author = "oksana.melnyk@example.com",
                                CreatedAt = DateTime.Now,
                                Title = "Підкресліть свій музичний стиль з бас-гітарою CORT Action V Plus! 🎸✨",
                                Content = "🎶 Потужний звук: За допомогою високоякісних звукознімачів та інноваційної технології, ця бас-гітара забезпечить вам глибокі баси та яскраві високі ноти, дозволяючи вам створювати неповторну музику.\r\n\r\n🎸 Зручність гри: Зручна форма та ергономічний дизайн роблять гру на цій бас-гітарі легкою та приємною, навіть протягом тривалого виконання.\r\n\r\n🎵 Вибір професіоналів: CORT Action V Plus - це вибір багатьох професійних музикантів, що шукають якість та надійність у своєму інструменті.\r\n\r\n🔊 Виразність звуку: Завдяки своїй чудовій звукопередачі та динамічному діапазону, ця бас-гітара дозволить вам виразно виразити свої музичні ідеї.\r\n\r\nЗануртеся у світ музики з бас-гітарою CORT Action V Plus та створіть музичні шедеври! 🎶🌟",
                                image = "CORT Action V Plus.webp",
                            }
                        },
                        Orders = new List<Orders> {
                            new Orders
                            {
                                TotalPrice = LIGHTWAVE.Price,
                                Date = DateTime.Now,
                                DeliveryAddress = "Проспект Миру, 5, Луцьк",
                                Delivery = novaposhta,
                                DeliveryId = novaposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = LIGHTWAVE.Id,
                                        Products = LIGHTWAVE,
                                        Quantity = 1,
                                        Status = 3,
                                    }
                                }
                            },
                            new Orders
                            {
                                TotalPrice = HatorHellraizer.Price,
                                Date = DateTime.Now.AddDays(-2),
                                DeliveryAddress = "Проспект Миру, 5, Луцьк",
                                Delivery = novaposhta,
                                DeliveryId = novaposhta.Id,
                                OrdersProducts = new List<OrdersProducts>
                                {
                                    new OrdersProducts
                                    {
                                        ProductId = HatorHellraizer.Id,
                                        Products = HatorHellraizer,
                                        Quantity = 1,
                                        Status = 0
                                    }
                                }
                            }
                        }
                    };

                    await context.AddAsync(user10);

                    var user11 = new Users
                    {
                        Email = "andrey.fedorenko@example.com",
                        Password = PasswordManager.HashPassword("11AAandrey"),
                        RoleId = moderatorRole.Id,
                        Roles = moderatorRole,
                        PhoneNumber = "380956784512",
                        Surname = "Федоренко",
                        Name = "Андрій",
                        Patronimic = "Віталійович",
                    };

                    await context.AddAsync(user11);

                    var user12 = new Users
                    {
                        Email = "viktor.pavlenko@example.com",
                        Password = PasswordManager.HashPassword("11AAviktor"),
                        RoleId = adminRole.Id,
                        Roles = adminRole,
                        PhoneNumber = "380945678934",
                        Surname = "Павленко",
                        Name = "Віктор",
                        Patronimic = "Степанович",
                    };

                    await context.AddAsync(user12);

                    context.SaveChanges();
                }
            }
        }
    }
}
