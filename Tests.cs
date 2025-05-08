using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba8
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var agency = new RealEstateAgency();

            var client1 = new Client("Андрій", "Осадчук", "+380984377209", "UA123456789");
            var client2 = new Client("Нікіта", "Грубін", "+380652525252", "UA252525252");
            var client3 = new Client("Єлизавета", "Швецова", "+380971237698", "UA111222333");
            var client4 = new Client("Акакій", "Окон", "+380685426643", "UA64981345");

            var property1 = new Property("вул. Київська 96", PropertyType.OneRoomApartment, 15000, "Затишна квартира");
            var property2 = new Property("вул. Житомирська 52", PropertyType.TwoRoomApartment, 12000, "Простора");
            var property3 = new Property("вул. Східна 67", PropertyType.PrivateLand, 24000, "");
            var property4 = new Property("вул. Покровська 28", PropertyType.ThreeRoomApartment, 52000, "В новобудові");
            var property5 = new Property("test", PropertyType.PrivateLand, 8000, "test");
            var property6 = new Property("test", PropertyType.ThreeRoomApartment, 13000, "test");

            // 1.1 Тестування додавання клієнта
            Console.WriteLine("1.1. Додавання клієнтів:");
            agency.AddClient(client1);
            Console.WriteLine($"Додано клієнта: {client1.FirstName} {client1.LastName}");
            agency.AddClient(client2);
            Console.WriteLine($"Додано клієнта: {client2.FirstName} {client2.LastName}");
            agency.AddClient(client3);
            Console.WriteLine($"Додано клієнта: {client3.FirstName} {client3.LastName}");
            agency.AddClient(client4);
            Console.WriteLine($"Додано клієнта: {client4.FirstName} {client4.LastName}");
            Console.WriteLine();

            // Тестування додавання клієнта з некоректними даними
            Console.WriteLine("Створення клієнта з порожнім банківським рахунком:");
            try
            {
                var client = new Client("Test", "Test", "Test", "");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 1.2 Тестування видалення клієнта
            Console.WriteLine("1.2. Видалення клієнта:");
            try
            {
                agency.RemoveClient(client4.Id);
                Console.WriteLine($"Видалено клієнта: {client4.FirstName} {client4.LastName}");
                agency.RemoveClient(client4.Id);
                Console.WriteLine($"Видалено клієнта: {client4.FirstName} {client4.LastName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 1.5 Тестування перегляду списку клієнтів
            Console.WriteLine("1.5. Список клієнтів:");
            agency.GetAllClients();
            Console.WriteLine();

            // 1.3 Тестування змінювання даних клієнта
            Console.WriteLine("1.3. Зміна даних клієнтів:");
            try
            {
                agency.UpdateClient(client2.Id, "Микита", "Губін", "+380936073122", "UA252525252");
                Console.WriteLine($"Оновлені дані клієнта: {client2.FirstName} {client2.LastName}, тел. {client2.PhoneNumber}, банківський рахунок: {client2.BankAccount}");
                agency.UpdateClient(client4.Id, "Микита", "Губін", "+380936073122", "UA252525252");
                Console.WriteLine($"Оновлені дані клієнта: {client2.FirstName} {client2.LastName}, тел. {client2.PhoneNumber}, банківський рахунок: {client2.BankAccount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 1.4 Тестування перегляду клієнта
            Console.WriteLine("1.4. Перегляд даних клієнта:");
            try
            {
                agency.GetClient(client3.Id);
                Console.WriteLine($"Клієнт: {client3.FirstName} {client3.LastName}, тел. {client3.PhoneNumber}, банківський рахунок: {client3.BankAccount}");
                agency.GetClient(client4.Id);
                Console.WriteLine($"Клієнт: {client4.FirstName} {client4.LastName}, тел. {client4.PhoneNumber}, банківський рахунок: {client4.BankAccount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 1.5.1-3 Тестування сортування списку клієнтів
            Console.WriteLine("1.5.1 Сортування клієнтів по імені:");
            var clientsSortByFirstName = agency.SortClients("firstname");
            foreach(var client in clientsSortByFirstName)
                Console.WriteLine($"{client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
            Console.WriteLine();

            Console.WriteLine("1.5.2 Сортування клієнтів по прізвищу:");
            try
            {
                var clientsSortByLastName = agency.SortClients("lastname");
                foreach (var client in clientsSortByLastName)
                    Console.WriteLine($"{client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
                Console.WriteLine();
                var clientsSortby = agency.SortClients("last");
                foreach (var client in clientsSortby)
                    Console.WriteLine($"{client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            
            Console.WriteLine();

            Console.WriteLine("1.5.3 Сортування клієнтів по початковій цифрі банківського рахунку:");
            var clientsSortByAccount = agency.SortClients("bankaccount");
            foreach (var client in clientsSortByAccount)
                Console.WriteLine($"{client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
            Console.WriteLine();

            // 2.1 Тестування додавання об'єктів нерухомості
            Console.WriteLine("2.1. Дадавання об'єкта нерухомості:");
            try
            {
                agency.AddProperty(property1);
                Console.WriteLine($"Додано об'єкт: {property1.Address}, {property1.Price} UAH, {property1.Type}, {property1.Description}");
                agency.AddProperty(property2);
                Console.WriteLine($"Додано об'єкт: {property2.Address}, {property2.Price} UAH, {property2.Type}, {property2.Description}");
                agency.AddProperty(property3);
                Console.WriteLine($"Додано об'єкт: {property3.Address}, {property3.Price} UAH, {property3.Type}, {property3.Description}");
                agency.AddProperty(property4);
                Console.WriteLine($"Додано об'єкт: {property4.Address}, {property4.Price} UAH, {property4.Type}, {property4.Description}");
                agency.AddProperty(property5);
                Console.WriteLine($"Додано об'єкт: {property5.Address}, {property5.Price} UAH, {property5.Type}, {property5.Description}");
                agency.AddProperty(property6);
                Console.WriteLine($"Додано об'єкт: {property6.Address}, {property6.Price} UAH, {property6.Type}, {property6.Description}");
                agency.AddProperty(property4);
                Console.WriteLine($"Додано об'єкт: {property4.Address}, {property4.Price} UAH, {property4.Type}, {property4.Description}");
                var property10 = new Property(null, PropertyType.ThreeRoomApartment, 52000, "В новобудові");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 2.2 Тестування видалення об'єктів нерухомості
            Console.WriteLine("2.2. Видалення об'єкта нерухомості:");
            try
            {
                agency.RemoveProperty(property4.Id);
                Console.WriteLine($"Видалено об'єкт: {property4.Address}, {property4.Price} UAH, {property4.Type}, {property4.Description}");
                agency.RemoveProperty(property4.Id);
                Console.WriteLine($"Видалено об'єкт: {property4.Address}, {property4.Price} UAH, {property4.Type}, {property4.Description}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 2.3 Тестування зміни даних об'єкта нерухомості
            Console.WriteLine("2.3. Зміна даних об'єкта нерухомості:");
            try
            {
                agency.UpdateProperty(property3.Id, property3.Address, property3.Type, 20000, "Дорога");
                Console.WriteLine($"Видалено об'єкт: {property3.Address}, {property3.Price} UAH, {property3.Type}, {property3.Description}");
                agency.UpdateProperty(property4.Id, property3.Address, property3.Type, 20000, "Дорога");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 2.4 Тестування перегляду даних конкретного об'єкта нерухомості
            Console.WriteLine("2.4. Перегляд даних конкретного об'єкта нерухомості:");
            try
            {
                agency.GetProperty(property2.Id);
                Console.WriteLine($"Об'єкт: {property2.Address}, {property2.Price} UAH, {property2.Type}, {property2.Description}");
                agency.GetProperty(property4.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 2.5 Тестування перегляду списку всіх об'єктів нерухомості
            Console.WriteLine("2.5. Список Всіх об'єктів нерухомості:");
            agency.GetAllProperties();
            Console.WriteLine();

            // 2.5.1-2 Тестування сортування списку об'єктів нерухомості
            Console.WriteLine("2.5.1-2 Сортування списку об'єктів нерухомості за типом та за ціною:\n");
            try
            {
                Console.WriteLine("2.5.1 Сортування за типом:");
                var propertiesSortByType = agency.SortProperties("type");
                foreach (var property in propertiesSortByType)
                    Console.WriteLine($"{property.Address}, {property.Type}, {property.Price} UAH, {property.Description}");
                Console.WriteLine();

                var propertiesSortByPrice = agency.SortProperties("price");
                Console.WriteLine("2.5.2 Сортування за ціною:");
                foreach (var property in propertiesSortByPrice)
                    Console.WriteLine($"{property.Address}, {property.Type}, {property.Price} UAH, {property.Description}");
                Console.WriteLine();

                var propertiesSortBy = agency.SortProperties("tung");
                foreach (var property in propertiesSortBy)
                    Console.WriteLine($"{property.Address}, {property.Type}, {property.Price} UAH, {property.Description}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // Створення пропозиції
            Console.WriteLine("Створення пропозиції:");
            var offer1 = agency.CreateOffer(client1);
            Console.WriteLine($"Створено пропозицію для клієнта: {offer1.Client.FirstName} {offer1.Client.LastName}");
            try
            {
                var offer2 = agency.CreateOffer(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 3.1 Тестування додавання пропозицій
            Console.WriteLine("3.1. Додавання об'єктів до пропозиції:");
            try
            {
                var availableProperties = agency.Properties;
                if (availableProperties.Any())
                {
                    offer1.AddProperty(agency.Properties[0]);
                    var count = offer1.OfferedProperties.Count;
                    Console.WriteLine($"Для {offer1.Client.FirstName} {offer1.Client.LastName} додано пропозицію: {offer1.OfferedProperties[count - 1].Address}, {offer1.OfferedProperties[count - 1].Price} UAH, {offer1.OfferedProperties[count - 1].Type}, {offer1.OfferedProperties[count - 1].Description}");
                    offer1.AddProperty(agency.Properties[1]);
                    count = offer1.OfferedProperties.Count;
                    Console.WriteLine($"Для {offer1.Client.FirstName} {offer1.Client.LastName} додано пропозицію: {offer1.OfferedProperties[count - 1].Address}, {offer1.OfferedProperties[count - 1].Price} UAH, {offer1.OfferedProperties[count - 1].Type}, {offer1.OfferedProperties[count - 1].Description}");
                    offer1.AddProperty(agency.Properties[2]);
                    count = offer1.OfferedProperties.Count;
                    Console.WriteLine($"Для {offer1.Client.FirstName} {offer1.Client.LastName} додано пропозицію: {offer1.OfferedProperties[count - 1].Address}, {offer1.OfferedProperties[count - 1].Price} UAH, {offer1.OfferedProperties[count - 1].Type}, {offer1.OfferedProperties[count - 1].Description}");
                    offer1.AddProperty(agency.Properties[3]);
                    count = offer1.OfferedProperties.Count;
                    Console.WriteLine($"Для {offer1.Client.FirstName} {offer1.Client.LastName} додано пропозицію: {offer1.OfferedProperties[count - 1].Address}, {offer1.OfferedProperties[count - 1].Price} UAH, {offer1.OfferedProperties[count - 1].Type}, {offer1.OfferedProperties[count - 1].Description}");
                    offer1.AddProperty(agency.Properties[4]);
                    count = offer1.OfferedProperties.Count;
                    Console.WriteLine($"Для {offer1.Client.FirstName} {offer1.Client.LastName} додано пропозицію: {offer1.OfferedProperties[count - 1].Address}, {offer1.OfferedProperties[count - 1].Price} UAH, {offer1.OfferedProperties[count - 1].Type}, {offer1.OfferedProperties[count - 1].Description}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 3.2 Чи бажаний об'єкт знаходиться в списту
            var offer3 = agency.CreateOffer(client3);
            Console.WriteLine("3.2. Перевірка чи є бажаний об'єкт в списку:");
            try
            {
                var suitableProperties = agency.FindSuitableProperties(PropertyType.OneRoomApartment, 15000);
                if (suitableProperties.Any())
                {
                    Console.WriteLine("Знайдені об’єкти, що відповідають вимогам:");
                    foreach (var property in suitableProperties)
                    {
                        offer3.AddProperty(property);
                        Console.WriteLine($"Знайдений і доданий до пропозиції для {offer3.Client.FirstName} {offer3.Client.LastName} об'єкт: {property.Address}, {property.Price} UAH, {property.Type}, {property.Description}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 3.3 Тестування відхилення пропозиції
            Console.WriteLine("3.3. Відхилення пропозиції:");
            try
            {
                agency.RejectPropertyOffer(client1.Id, property3.Id);
                Console.WriteLine($"Видалено об'єкт з пропозиції для {client1.FirstName} {client1.LastName}: {property3.Address}, {property3.Price} UAH, {property3.Type}, {property3.Description}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("Список об'єктів в пропозиції:");
            offer1.GetAllPropertiesInOffer();
            Console.WriteLine();

            // 4.1 Тестування пошуку по ключовому слову серед клієнтів
            Console.WriteLine("4.1. Пошук по ключовому слову серед клієнтів:");
            string[] keywordsClients = { "Андрій", "+380971237698" };
            try
            {
                foreach(var keyword in keywordsClients)
                {
                    Console.WriteLine($"Пошук за ключовим словом: '{keyword}'");
                    var foundClients = agency.SearchClients(keyword);
                    foreach (var client in foundClients)
                    {
                        Console.WriteLine($"Знайдено клієнта: {client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 4.2 Тестування пошуку по ключовому слову серед об'єктів нерухомості
            Console.WriteLine("4.2. Пошук по ключовому слову серед об'єктів нерухомості:");
            string[] keywordsProperty = { "Затишна", "Хрещатик" };
            try
            {
                foreach (var keyword in keywordsProperty)
                {
                    Console.WriteLine($"Пошук за ключовим словом: '{keyword}'");
                    var foundProperties = agency.SearchProperties(keyword);
                    foreach (var property in foundProperties)
                    {
                        Console.WriteLine($"Знайдено: {property.Address}, {property.Type}, {property.Price} UAH, {property.Description}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 4.3 Тестування пошуку по ключовому слову серед об'єктів нерухомості та клієнтів
            Console.WriteLine("4.3. Пошук по ключовому слову серед об'єктів нерухомості та клієнтів:");
            string[] keywordsAll = { "Затишна", "Микита", "Потужна" };
            try
            {
                foreach (var keyword in keywordsAll)
                {
                    Console.WriteLine($"Пошук за ключовим словом: '{keyword}'");
                    var foundItems = agency.SearchAll(keyword);
                    if (foundItems.Any())
                    {
                        foreach (var item in foundItems)
                        {
                            if (item is Client client)
                            {
                                Console.WriteLine($"Знайдено клієнта: {client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
                            }
                            else if (item is Property property)
                            {
                                Console.WriteLine($"Знайдено об’єкт: {property.Address}, {property.Type}, {property.Price} UAH, {property.Description}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();

            // 4.4 Тестування розширеного пошуку кліента
            Console.WriteLine("4.4. Розширений пошук клієнта:");
            try
            {
                var criteria = new ClientSearchCriteria
                {
                    FirstName = "Єлизавета",
                    DesiredPropertyType = PropertyType.OneRoomApartment
                };
                var foundClients = agency.AdvancedClientSearch(criteria);
                foreach (var client in foundClients)
                {
                    Console.WriteLine($"Знайдено: {client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
                }
                var criteria1 = new ClientSearchCriteria
                {
                    FirstName = "Губін",
                    PhoneNumber = "+380984377209"
                };
                foundClients = agency.AdvancedClientSearch(criteria1);
                foreach (var client in foundClients)
                {
                    Console.WriteLine($"Знайдено: {client.FirstName} {client.LastName}, тел. {client.PhoneNumber}, банківський рахунок: {client.BankAccount}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}
