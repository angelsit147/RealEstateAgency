using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Представляє клієнта в агентстві нерухомості.
/// </summary>
public class Client
{
    private string _bankAccount;

    /// <summary>
    /// Отримує унікальний ідентифікатор клієнта.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Отримує або встановлює ім'я клієнта.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Отримує або встановлює прізвище клієнта.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Отримує або встановлює номер телефону клієнта.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Отримує або встановлює номер банківського рахунку клієнта.
    /// </summary>
    /// <exception cref="ArgumentException">Викидається, якщо банківський рахунок є null, порожнім або складається з пробілів.</exception>
    public string BankAccount
    {
        get => _bankAccount;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Банківський рахунок не може бути порожнім або складатися з пробілів.");
            _bankAccount = value;
        }
    }

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="Client"/>.
    /// </summary>
    /// <param name="firstName">Ім'я клієнта. Не може бути null.</param>
    /// <param name="lastName">Прізвище клієнта. Не може бути null.</param>
    /// <param name="phoneNumber">Номер телефону клієнта. Не може бути null.</param>
    /// <param name="bankAccount">Номер банківського рахунку клієнта. Не може бути порожнім або складатися з пробілів.</param>
    /// <exception cref="ArgumentException">Викидається, якщо firstName, lastName, phoneNumber є null або bankAccount є некоректним.</exception>
    public Client(string firstName, string lastName, string phoneNumber, string bankAccount)
    {
        Id = Guid.NewGuid();
        if (firstName == null)
            throw new ArgumentException("Ім'я не може бути null.");
        FirstName = firstName;

        if (lastName == null)
            throw new ArgumentException("Прізвище не може бути null.");
        LastName = lastName;

        if (phoneNumber == null)
            throw new ArgumentException("Номер телефону не може бути null.");

        PhoneNumber = phoneNumber;
        BankAccount = bankAccount;
    }
}

/// <summary>
/// Визначає типи нерухомості, доступні в агентстві.
/// </summary>
public enum PropertyType
{
    OneRoomApartment,
    TwoRoomApartment,
    ThreeRoomApartment,
    PrivateLand
}

/// <summary>
/// Представляє об'єкт нерухомості в агентстві.
/// </summary>
public class Property
{
    /// <summary>
    /// Отримує унікальний ідентифікатор об'єкта нерухомості.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Отримує або встановлює адресу об'єкта нерухомості.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Отримує або встановлює тип об'єкта нерухомості.
    /// </summary>
    public PropertyType Type { get; set; }

    /// <summary>
    /// Отримує або встановлює ціну об'єкта нерухомості.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Отримує або встановлює опис об'єкта нерухомості.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="Property"/>.
    /// </summary>
    /// <param name="address">Адреса об'єкта нерухомості. Не може бути null.</param>
    /// <param name="type">Тип об'єкта нерухомості.</param>
    /// <param name="price">Ціна об'єкта нерухомості. Має бути додатною.</param>
    /// <param name="description">Опис об'єкта нерухомості. Не може бути null.</param>
    /// <exception cref="ArgumentException">Викидається, якщо адреса або опис є null, або ціна не є додатною.</exception>
    public Property(string address, PropertyType type, decimal price, string description)
    {
        if (price <= 0)
            throw new ArgumentException("Ціна має бути додатною.");

        Id = Guid.NewGuid();

        if (address == null)
            throw new ArgumentException("Адреса не може бути null.");
        Address = address;

        Type = type;
        Price = price;

        if (description == null)
            throw new ArgumentException("Опис не може бути null.");
        Description = description;
    }
}

/// <summary>
/// Представляє пропозицію нерухомості для клієнта, що містить список об'єктів нерухомості.
/// </summary>
public class PropertyOffer
{
    private readonly List<Property> _offeredProperties;

    /// <summary>
    /// Отримує клієнта, пов'язаного з цією пропозицією.
    /// </summary>
    public Client Client { get; private set; }

    /// <summary>
    /// Отримує список об'єктів нерухомості в пропозиції лише для читання.
    /// </summary>
    public IReadOnlyList<Property> OfferedProperties => _offeredProperties.AsReadOnly();

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="PropertyOffer"/>.
    /// </summary>
    /// <param name="client">Клієнт, для якого створено пропозицію. Не може бути null.</param>
    /// <exception cref="ArgumentException">Викидається, якщо клієнт є null.</exception>
    public PropertyOffer(Client client)
    {
        if (client == null)
            throw new ArgumentException("Клієнт не може бути null.");
        Client = client;

        _offeredProperties = new List<Property>();
    }

    /// <summary>
    /// Додає об'єкт нерухомості до пропозиції.
    /// </summary>
    /// <param name="property">Об'єкт нерухомості для додавання. Не може бути null.</param>
    /// <exception cref="ArgumentException">Викидається, якщо об'єкт нерухомості є null.</exception>
    /// <exception cref="InvalidOperationException">Викидається, якщо пропозиція вже містить 4 об'єкти або об'єкт уже є в пропозиції.</exception>
    public void AddProperty(Property property)
    {
        if (property == null)
            throw new ArgumentException("Об'єкт нерухомості не може бути null.");
        if (_offeredProperties.Count >= 4)
            throw new InvalidOperationException("Не можна додати більше 4 об'єктів до пропозиції.");
        if (_offeredProperties.Contains(property))
            throw new InvalidOperationException("Об'єкт уже є в пропозиції.");

        _offeredProperties.Add(property);
    }

    /// <summary>
    /// Видаляє об'єкт нерухомості з пропозиції.
    /// </summary>
    /// <param name="property">Об'єкт нерухомості для видалення. Не може бути null.</param>
    /// <exception cref="ArgumentException">Викидається, якщо об'єкт нерухомості є null.</exception>
    /// <exception cref="InvalidOperationException">Викидається, якщо об'єкт не знайдено в пропозиції.</exception>
    public void RemoveProperty(Property property)
    {
        if (property == null)
            throw new ArgumentException("Об'єкт нерухомості не може бути null.");
        if (!_offeredProperties.Remove(property))
            throw new InvalidOperationException("Об'єкт не знайдено в пропозиції.");
    }

    /// <summary>
    /// Виводить усі об'єкти нерухомості з пропозиції в консоль.
    /// </summary>
    public void GetAllPropertiesInOffer()
    {
        if (_offeredProperties.Count == 0)
        {
            Console.WriteLine("У пропозиції немає об'єктів нерухомості.");
            return;
        }
        foreach (var property in _offeredProperties)
        {
            Console.WriteLine($"Об'єкт: {property.Address}, {property.Price} грн, {property.Type}, {property.Description}");
        }
    }
}

/// <summary>
/// Визначає критерії пошуку для розширеного пошуку клієнтів.
/// </summary>
public class ClientSearchCriteria
{
    /// <summary>
    /// Отримує або встановлює прізвище для фільтрації клієнтів.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Отримує або встановлює ім'я для фільтрації клієнтів.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Отримує або встановлює номер телефону для фільтрації клієнтів.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Отримує або встановлює банківський рахунок для фільтрації клієнтів.
    /// </summary>
    public string? BankAccount { get; set; }

    /// <summary>
    /// Отримує або встановлює бажаний тип нерухомості для фільтрації клієнтів.
    /// </summary>
    public PropertyType? DesiredPropertyType { get; set; }

    /// <summary>
    /// Отримує або встановлює максимальну ціну для фільтрації клієнтів.
    /// </summary>
    public decimal? Price { get; set; }
}

/// <summary>
/// Визначає методи для пошуку клієнтів і об'єктів нерухомості в агентстві.
/// </summary>
public interface ISearchable
{
    /// <summary>
    /// Шукає клієнтів за ключовим словом.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку.</param>
    /// <returns>Перелік клієнтів, що відповідають умовам.</returns>
    IEnumerable<Client> SearchClients(string keyword);

    /// <summary>
    /// Шукає об'єкти нерухомості за ключовим словом.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку.</param>
    /// <returns>Перелік об'єктів нерухомості, що відповідають умовам.</returns>
    IEnumerable<Property> SearchProperties(string keyword);

    /// <summary>
    /// Шукає як клієнтів, так і об'єкти нерухомості за ключовим словом.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку.</param>
    /// <returns>Перелік клієнтів і об'єктів нерухомості як об'єктів, що відповідають умовам.</returns>
    IEnumerable<object> SearchAll(string keyword);
}

/// <summary>
/// Керує операціями, пов'язаними з клієнтами агентства нерухомості.
/// </summary>
public class ClientManager
{
    private readonly List<Client> _clients;

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="ClientManager"/>.
    /// </summary>
    public ClientManager()
    {
        _clients = new List<Client>();
    }

    /// <summary>
    /// Отримує список клієнтів лише для читання.
    /// </summary>
    public IReadOnlyList<Client> Clients => _clients.AsReadOnly();

    /// <summary>
    /// Додає нового клієнта до списку.
    /// </summary>
    /// <param name="client">Клієнт для додавання. Не може бути null.</param>
    /// <exception cref="ArgumentException">Викидається, якщо клієнт є null.</exception>
    /// <exception cref="InvalidOperationException">Викидається, якщо клієнт із таким ID уже існує.</exception>
    public void AddClient(Client client)
    {
        if (client == null)
            throw new ArgumentException("Клієнт не може бути null.");
        if (_clients.Any(c => c.Id == client.Id))
            throw new InvalidOperationException("Клієнт уже існує.");

        _clients.Add(client);
    }

    /// <summary>
    /// Видаляє клієнта за його унікальним ідентифікатором.
    /// </summary>
    /// <param name="clientId">Унікальний ідентифікатор клієнта.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо клієнт із вказаним ID не знайдений.</exception>
    public void RemoveClient(Guid clientId)
    {
        var client = _clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            throw new KeyNotFoundException("Клієнт не знайдений.");
        _clients.Remove(client);
    }

    /// <summary>
    /// Оновлює дані клієнта за його унікальним ідентифікатором.
    /// </summary>
    /// <param name="clientId">Унікальний ідентифікатор клієнта.</param>
    /// <param name="firstName">Нове ім'я клієнта.</param>
    /// <param name="lastName">Нове прізвище клієнта.</param>
    /// <param name="phoneNumber">Новий номер телефону клієнта.</param>
    /// <param name="bankAccount">Новий номер банківського рахунку клієнта.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо клієнт із вказаним ID не знайдений.</exception>
    public void UpdateClient(Guid clientId, string firstName, string lastName, string phoneNumber, string bankAccount)
    {
        var client = _clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            throw new KeyNotFoundException("Клієнт не знайдений.");

        client.FirstName = firstName;
        client.LastName = lastName;
        client.PhoneNumber = phoneNumber;
        client.BankAccount = bankAccount;
    }

    /// <summary>
    /// Отримує клієнта за його унікальним ідентифікатором.
    /// </summary>
    /// <param name="clientId">Унікальний ідентифікатор клієнта.</param>
    /// <returns>Клієнт із вказаним ID.</returns>
    /// <exception cref="KeyNotFoundException">Викидається, якщо клієнт із вказаним ID не знайдений.</exception>
    public Client GetClient(Guid clientId)
    {
        var client = _clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            throw new KeyNotFoundException("Клієнт не знайдений.");
        return client;
    }

    /// <summary>
    /// Виводить список усіх клієнтів у консоль.
    /// </summary>
    public void GetAllClients()
    {
        var clients = _clients.ToList();
        foreach (var client in clients)
            Console.WriteLine($"{client.FirstName} {client.LastName}, Телефон: {client.PhoneNumber}, Банківський рахунок: {client.BankAccount}");
    }

    /// <summary>
    /// Сортує клієнтів за вказаним критерієм.
    /// </summary>
    /// <param name="sortBy">Критерій сортування ("firstname", "lastname", "bankaccount").</param>
    /// <returns>Відсортований перелік клієнтів.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо критерій сортування некоректний.</exception>
    public IEnumerable<Client> SortClients(string? sortBy = null)
    {
        var clients = _clients.AsEnumerable();
        switch (sortBy?.ToLower())
        {
            case "firstname":
                return clients.OrderBy(c => c.FirstName);
            case "lastname":
                return clients.OrderBy(c => c.LastName);
            case "bankaccount":
                return clients.OrderBy(c => c.BankAccount[2..]);
            default:
                throw new ArgumentException("Некоректний параметр сортування.");
        }
    }
}

/// <summary>
/// Керує операціями, пов'язаними з об'єктами нерухомості в агентстві.
/// </summary>
public class PropertyManager
{
    private readonly List<Property> _properties;

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="PropertyManager"/>.
    /// </summary>
    public PropertyManager()
    {
        _properties = new List<Property>();
    }

    /// <summary>
    /// Отримує список об'єктів нерухомості лише для читання.
    /// </summary>
    public IReadOnlyList<Property> Properties => _properties.AsReadOnly();

    /// <summary>
    /// Додає новий об'єкт нерухомості до списку.
    /// </summary>
    /// <param name="property">Об'єкт нерухомості для додавання. Не може бути null.</param>
    /// <exception cref="ArgumentNullException">Викидається, якщо об'єкт нерухомості є null.</exception>
    /// <exception cref="InvalidOperationException">Викидається, якщо об'єкт із таким ID уже існує.</exception>
    public void AddProperty(Property property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));
        if (_properties.Any(p => p.Id == property.Id))
            throw new InvalidOperationException("Об'єкт нерухомості уже існує.");

        _properties.Add(property);
    }

    /// <summary>
    /// Видаляє об'єкт нерухомості за його унікальним ідентифікатором.
    /// </summary>
    /// <param name="propertyId">Унікальний ідентифікатор об'єкта нерухомості.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо об'єкт із вказаним ID не знайдений.</exception>
    public void RemoveProperty(Guid propertyId)
    {
        var property = _properties.FirstOrDefault(p => p.Id == propertyId);
        if (property == null)
            throw new KeyNotFoundException("Об'єкт нерухомості не знайдений.");
        _properties.Remove(property);
    }

    /// <summary>
    /// Оновлює дані об'єкта нерухомості за його унікальним ідентифікатором.
    /// </summary>
    /// <param name="propertyId">Унікальний ідентифікатор об'єкта нерухомості.</param>
    /// <param name="address">Нова адреса об'єкта.</param>
    /// <param name="type">Новий тип об'єкта.</param>
    /// <param name="price">Нова ціна об'єкта.</param>
    /// <param name="description">Новий опис об'єкта.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо об'єкт із вказаним ID не знайдений.</exception>
    public void UpdateProperty(Guid propertyId, string address, PropertyType type, decimal price, string description)
    {
        var property = _properties.FirstOrDefault(p => p.Id == propertyId);
        if (property == null)
            throw new KeyNotFoundException("Об'єкт нерухомості не знайдений.");

        property.Address = address;
        property.Type = type;
        property.Price = price;
        property.Description = description;
    }

    /// <summary>
    /// Отримує об'єкт нерухомості за його унікальним ідентифікатором.
    /// </summary>
    /// <param name="propertyId">Унікальний ідентифікатор об'єкта нерухомості.</param>
    /// <returns>Об'єкт нерухомості із вказаним ID.</returns>
    /// <exception cref="KeyNotFoundException">Викидається, якщо об'єкт із вказаним ID не знайдений.</exception>
    public Property GetProperty(Guid propertyId)
    {
        var property = _properties.FirstOrDefault(p => p.Id == propertyId);
        if (property == null)
            throw new KeyNotFoundException("Об'єкт нерухомості не знайдений.");
        return property;
    }

    /// <summary>
    /// Виводить список усіх об'єктів нерухомості у консоль.
    /// </summary>
    public void GetAllProperties()
    {
        var properties = _properties.ToList();
        foreach (var property in properties)
            Console.WriteLine($"Об'єкт: {property.Address}, {property.Price} грн, {property.Type}, {property.Description}");
    }

    /// <summary>
    /// Сортує об'єкти нерухомості за вказаним критерієм.
    /// </summary>
    /// <param name="sortBy">Критерій сортування ("type", "price").</param>
    /// <returns>Відсортований перелік об'єктів нерухомості.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо критерій сортування некоректний.</exception>
    public IEnumerable<Property> SortProperties(string? sortBy = null)
    {
        var properties = _properties.AsEnumerable();
        switch (sortBy?.ToLower())
        {
            case "type":
                return properties.OrderBy(p => p.Type);
            case "price":
                return properties.OrderBy(p => p.Price);
            default:
                throw new ArgumentException("Некоректний параметр сортування.");
        }
    }
}

/// <summary>
/// Керує пропозиціями нерухомості для клієнтів агентства.
/// </summary>
public class OfferManager
{
    private readonly List<PropertyOffer> _offers;
    private readonly PropertyManager _propertyManager;

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="OfferManager"/>.
    /// </summary>
    /// <param name="propertyManager">Менеджер об'єктів нерухомості. Не може бути null.</param>
    /// <exception cref="ArgumentNullException">Викидається, якщо propertyManager є null.</exception>
    public OfferManager(PropertyManager propertyManager)
    {
        _offers = new List<PropertyOffer>();
        _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
    }

    /// <summary>
    /// Отримує список пропозицій лише для читання.
    /// </summary>
    /// <returns>Список пропозицій.</returns>
    public IReadOnlyList<PropertyOffer> GetOffers()
    {
        return _offers.AsReadOnly();
    }

    /// <summary>
    /// Створює нову пропозицію для клієнта.
    /// </summary>
    /// <param name="client">Клієнт, для якого створюється пропозиція. Не може бути null.</param>
    /// <returns>Створена пропозиція.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо клієнт є null.</exception>
    public PropertyOffer CreateOffer(Client client)
    {
        var offer = new PropertyOffer(client);
        _offers.Add(offer);
        return offer;
    }

    /// <summary>
    /// Видаляє об'єкт нерухомості з пропозиції клієнта.
    /// </summary>
    /// <param name="clientId">Унікальний ідентифікатор клієнта.</param>
    /// <param name="propertyId">Унікальний ідентифікатор об'єкта нерухомості.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо пропозиція для клієнта не знайдена.</exception>
    /// <exception cref="InvalidOperationException">Викидається, якщо об'єкт не знайдено в пропозиції.</exception>
    public void RejectPropertyOffer(Guid clientId, Guid propertyId)
    {
        var offer = _offers.FirstOrDefault(o => o.Client.Id == clientId);
        if (offer == null)
            throw new KeyNotFoundException("Пропозиція для клієнта не знайдена.");
        var property = offer.OfferedProperties.FirstOrDefault(p => p.Id == propertyId);
        if (property == null)
            throw new InvalidOperationException("Об'єкт не знайдено в пропозиції.");
        offer.RemoveProperty(property);
    }

    /// <summary>
    /// Знаходить об'єкти нерухомості, що відповідають вказаному типу та ціні.
    /// </summary>
    /// <param name="type">Тип об'єкта нерухомості.</param>
    /// <param name="price">Максимальна ціна об'єкта.</param>
    /// <returns>Перелік відповідних об'єктів нерухомості.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо не знайдено об'єктів, що відповідають критеріям.</exception>
    public IEnumerable<Property> FindSuitableProperties(PropertyType type, decimal price)
    {
        var suitableProperties = _propertyManager.Properties
            .Where(p => p.Type == type && p.Price <= price);
        if (!suitableProperties.Any())
        {
            throw new ArgumentException("Не знайдено об'єктів, що відповідають критеріям.");
        }
        return suitableProperties;
    }
}

/// <summary>
/// Керує пошуком клієнтів і об'єктів нерухомості в агентстві.
/// </summary>
public class SearchManager : ISearchable
{
    private readonly ClientManager _clientManager;
    private readonly PropertyManager _propertyManager;
    private readonly OfferManager _offerManager;

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="SearchManager"/>.
    /// </summary>
    /// <param name="clientManager">Менеджер клієнтів. Не може бути null.</param>
    /// <param name="propertyManager">Менеджер об'єктів нерухомості. Не може бути null.</param>
    /// <param name="offerManager">Менеджер пропозицій. Не може бути null.</param>
    /// <exception cref="ArgumentNullException">Викидається, якщо будь-який із менеджерів є null.</exception>
    public SearchManager(ClientManager clientManager, PropertyManager propertyManager, OfferManager offerManager)
    {
        if (clientManager == null)
        {
            throw new ArgumentNullException(nameof(clientManager));
        }
        _clientManager = clientManager;

        if (propertyManager == null)
        {
            throw new ArgumentNullException(nameof(propertyManager));
        }
        _propertyManager = propertyManager;

        if (offerManager == null)
        {
            throw new ArgumentNullException(nameof(offerManager));
        }
        _offerManager = offerManager;
    }

    /// <summary>
    /// Шукає клієнтів за ключовим словом.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку.</param>
    /// <returns>Перелік клієнтів, що відповідають умовам.</returns>
    public IEnumerable<Client> SearchClients(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Enumerable.Empty<Client>();

        var results = _clientManager.Clients.Where(c =>
            (c.FirstName != null && c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
            (c.LastName != null && c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
            (c.PhoneNumber != null && c.PhoneNumber.Contains(keyword)) ||
            (c.BankAccount != null && c.BankAccount.Contains(keyword)));

        if (!results.Any())
            throw new ArgumentException($"Клієнтів не знайдено за ключовим словом '{keyword}'.");

        return results;
    }

    /// <summary>
    /// Шукає об'єкти нерухомості за ключовим словом.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку.</param>
    /// <returns>Перелік об'єктів нерухомості, що відповідають умовам.</returns>
    public IEnumerable<Property> SearchProperties(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Enumerable.Empty<Property>();

        bool isPrice = decimal.TryParse(keyword, out decimal price);

        bool isType = Enum.TryParse<PropertyType>(keyword, true, out PropertyType propertyType);

        var results = _propertyManager.Properties.Where(p =>
                (p.Address != null && p.Address.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                (p.Description != null && p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                (isPrice && p.Price == price) ||
                (isType && p.Type == propertyType));

        if (!results.Any())
            throw new ArgumentException($"Об'єкти нерухомості не знайдені за ключовим словом '{keyword}'.");

        return results;
    }

    /// <summary>
    /// Шукає як клієнтів, так і об'єкти нерухомості за ключовим словом.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку.</param>
    /// <returns>Перелік клієнтів і об'єктів нерухомості як об'єктів, що відповідають умовам.</returns>
    public IEnumerable<object> SearchAll(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Enumerable.Empty<object>();

        IEnumerable<object> clientResults = Enumerable.Empty<object>();
        IEnumerable<object> propertyResults = Enumerable.Empty<object>();

        try
        {
            clientResults = SearchClients(keyword).Cast<object>();
        }
        catch (ArgumentException) { }

        try
        {
            propertyResults = SearchProperties(keyword).Cast<object>();
        }
        catch (ArgumentException) { }

        var results = clientResults.Concat(propertyResults);
        if (!results.Any())
            throw new ArgumentException($"Результати не знайдені за ключовим словом '{keyword}'.");

        return results;
    }

    /// <summary>
    /// Виконує розширений пошук клієнтів за заданими критеріями.
    /// </summary>
    /// <param name="criteria">Критерії пошуку клієнтів. Не може бути null.</param>
    /// <returns>Перелік клієнтів, що відповідають критеріям.</returns>
    /// <exception cref="ArgumentNullException">Викидається, якщо критерії є null.</exception>
    /// <exception cref="ArgumentException">Викидається, якщо клієнтів не знайдено за критеріями.</exception>
    public IEnumerable<Client> AdvancedClientSearch(ClientSearchCriteria criteria)
    {
        if (criteria == null)
            throw new ArgumentNullException(nameof(criteria));

        IEnumerable<Client> results = _clientManager.Clients;

        if (!string.IsNullOrEmpty(criteria.FirstName))
        {
            results = results.Where(c => c.FirstName != null &&
                c.FirstName.Equals(criteria.FirstName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(criteria.LastName))
        {
            results = results.Where(c => c.LastName != null &&
                c.LastName.Equals(criteria.LastName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(criteria.PhoneNumber))
        {
            var phoneResults = SearchClients(criteria.PhoneNumber);
            results = results.Intersect(phoneResults);
        }

        if (!string.IsNullOrEmpty(criteria.BankAccount))
        {
            var bankResults = SearchClients(criteria.BankAccount);
            results = results.Intersect(bankResults);
        }

        if (criteria.DesiredPropertyType != null)
        {
            results = results.Where(c => _offerManager.GetOffers().Any(o =>
                o.Client.Id == c.Id &&
                o.OfferedProperties.Any(p => p.Type == criteria.DesiredPropertyType)));
        }

        if (criteria.Price != null)
        {
            results = results.Where(c => _offerManager.GetOffers().Any(o =>
                o.Client.Id == c.Id &&
                o.OfferedProperties.Any(p => p.Price <= criteria.Price)));
        }

        if (!results.Any())
            throw new ArgumentException("Клієнтів не знайдено за зазначеними критеріями.");

        return results;
    }
}