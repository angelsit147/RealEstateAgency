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
/// Представляє агентство нерухомості, яке керує клієнтами, об'єктами нерухомості та пропозиціями.
/// </summary>
public class RealEstateAgency : ISearchable
{
    private readonly List<Client> _clients;
    private readonly List<Property> _properties;
    private readonly List<PropertyOffer> _offers;

    /// <summary>
    /// Ініціалізує новий екземпляр класу <see cref="RealEstateAgency"/>.
    /// </summary>
    public RealEstateAgency()
    {
        _clients = new List<Client>();
        _properties = new List<Property>();
        _offers = new List<PropertyOffer>();
    }

    #region Client Management

    /// <summary>
    /// Додає клієнта до агентства.
    /// </summary>
    /// <param name="client">Клієнт для додавання. Не може бути null.</param>
    /// <exception cref="ArgumentException">Викидається, якщо клієнт є null.</exception>
    /// <exception cref="InvalidOperationException">Викидається, якщо клієнт з таким ID уже існує.</exception>
    public void AddClient(Client client)
    {
        if (client == null)
            throw new ArgumentException("Клієнт не може бути null.");
        if (_clients.Any(c => c.Id == client.Id))
            throw new InvalidOperationException("Клієнт уже існує.");

        _clients.Add(client);
    }

    /// <summary>
    /// Видаляє клієнта з агентства за його ID.
    /// </summary>
    /// <param name="clientId">ID клієнта для видалення.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо клієнт не знайдений.</exception>
    public void RemoveClient(Guid clientId)
    {
        var client = _clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            throw new KeyNotFoundException("Клієнт не знайдений.");
        _clients.Remove(client);
    }

    /// <summary>
    /// Оновлює дані існуючого клієнта.
    /// </summary>
    /// <param name="clientId">ID клієнта для оновлення.</param>
    /// <param name="firstName">Нове ім'я.</param>
    /// <param name="lastName">Нове прізвище.</param>
    /// <param name="phoneNumber">Новий номер телефону.</param>
    /// <param name="bankAccount">Новий номер банківського рахунку.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо клієнт не знайдений.</exception>
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
    /// Отримує клієнта за його ID.
    /// </summary>
    /// <param name="clientId">ID клієнта для отримання.</param>
    /// <returns>Клієнт із зазначеним ID.</returns>
    /// <exception cref="KeyNotFoundException">Викидається, якщо клієнт не знайдений.</exception>
    public Client GetClient(Guid clientId)
    {
        var client = _clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            throw new KeyNotFoundException("Клієнт не знайдений.");
        return client;
    }

    /// <summary>
    /// Виводить усіх клієнтів у консоль.
    /// </summary>
    public void GetAllClients()
    {
        var clients = _clients.ToList();
        foreach (var client in clients)
            Console.WriteLine($"{client.FirstName} {client.LastName}, Телефон: {client.PhoneNumber}, Банківський рахунок: {client.BankAccount}");
    }

    /// <summary>
    /// Сортує клієнтів за зазначеним критерієм.
    /// </summary>
    /// <param name="sortBy">Критерій сортування ("firstname", "lastname" або "bankaccount").</param>
    /// <returns>Перелік відсортованих клієнтів.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо параметр сортування є некоректним.</exception>
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
    #endregion

    #region Property Management

    /// <summary>
    /// Додає об'єкт нерухомості до агентства.
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
    /// Видаляє об'єкт нерухомості з агентства за його ID.
    /// </summary>
    /// <param name="propertyId">ID об'єкта нерухомості для видалення.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо об'єкт нерухомості не знайдений.</exception>
    public void RemoveProperty(Guid propertyId)
    {
        var property = _properties.FirstOrDefault(p => p.Id == propertyId);
        if (property == null)
            throw new KeyNotFoundException("Об'єкт нерухомості не знайдений.");
        _properties.Remove(property);
    }

    /// <summary>
    /// Оновлює дані існуючого об'єкта нерухомості.
    /// </summary>
    /// <param name="propertyId">ID об'єкта нерухомості для оновлення.</param>
    /// <param name="address">Нова адреса.</param>
    /// <param name="type">Новий тип об'єкта нерухомості.</param>
    /// <param name="price">Нова ціна.</param>
    /// <param name="description">Новий опис.</param>
    /// <exception cref="KeyNotFoundException">Викидається, якщо об'єкт нерухомості не знайдений.</exception>
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
    /// Отримує список усіх об'єктів нерухомості в агентстві лише для читання.
    /// </summary>
    public IReadOnlyList<Property> Properties => _properties.AsReadOnly();

    /// <summary>
    /// Отримує об'єкт нерухомості за його ID.
    /// </summary>
    /// <param name="propertyId">ID об'єкта нерухомості для отримання.</param>
    /// <returns>Об'єкт нерухомості із зазначеним ID.</returns>
    /// <exception cref="KeyNotFoundException">Викидається, якщо об'єкт нерухомості не знайдений.</exception>
    public Property GetProperty(Guid propertyId)
    {
        var property = _properties.FirstOrDefault(p => p.Id == propertyId);
        if (property == null)
            throw new KeyNotFoundException("Об'єкт нерухомості не знайдений.");
        return property;
    }

    /// <summary>
    /// Виводить усі об'єкти нерухомості в консоль.
    /// </summary>
    public void GetAllProperties()
    {
        var properties = _properties.ToList();
        foreach (var property in properties)
            Console.WriteLine($"Об'єкт: {property.Address}, {property.Price} грн, {property.Type}, {property.Description}");
    }

    /// <summary>
    /// Сортує об'єкти нерухомості за зазначеним критерієм.
    /// </summary>
    /// <param name="sortBy">Критерій сортування ("type" або "price").</param>
    /// <returns>Перелік відсортованих об'єктів нерухомості.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо параметр сортування є некоректним.</exception>
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
    #endregion

    #region Offer Management

    /// <summary>
    /// Створює нову пропозицію нерухомості для клієнта.
    /// </summary>
    /// <param name="client">Клієнт, для якого створено пропозицію.</param>
    /// <returns>Створена пропозиція нерухомості.</returns>
    public PropertyOffer CreateOffer(Client client)
    {
        var offer = new PropertyOffer(client);
        _offers.Add(offer);
        return offer;
    }

    /// <summary>
    /// Видаляє об'єкт нерухомості з пропозиції клієнта.
    /// </summary>
    /// <param name="clientId">ID клієнта, чия пропозиція змінюється.</param>
    /// <param name="propertyId">ID об'єкта нерухомості для видалення.</param>
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
    /// Знаходить об'єкти нерухомості, які відповідають зазначеному типу та ціні.
    /// </summary>
    /// <param name="type">Тип об'єкта нерухомості для пошуку.</param>
    /// <param name="Price">Максимальна ціна об'єктів нерухомості.</param>
    /// <returns>Перелік об'єктів нерухомості, що відповідають умовам.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо жоден об'єкт не відповідає критеріям.</exception>
    public IEnumerable<Property> FindSuitableProperties(PropertyType type, decimal Price)
    {
        var suitableProperties = _properties.Where(p => p.Type == type && p.Price <= Price);
        if (!suitableProperties.Any())
        {
            throw new ArgumentException("Не знайдено об'єктів, що відповідають критеріям.");
        }
        return suitableProperties;
    }
    #endregion

    #region Search

    /// <summary>
    /// Шукає клієнтів за ключовим словом у їхньому імені, прізвищі, номері телефону або банківському рахунку.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку. Не може бути порожнім або складатися з пробілів.</param>
    /// <returns>Перелік клієнтів, що відповідають умовам.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо клієнти не знайдені або ключове слово є некоректним.</exception>
    public IEnumerable<Client> SearchClients(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Enumerable.Empty<Client>();

        var results = _clients.Where(c =>
            (c.FirstName != null && c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
            (c.LastName != null && c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
            (c.PhoneNumber != null && c.PhoneNumber.Contains(keyword)) ||
            (c.BankAccount != null && c.BankAccount.Contains(keyword)));

        if (!results.Any())
            throw new ArgumentException($"Клієнтів не знайдено за ключовим словом '{keyword}'.");

        return results;
    }

    /// <summary>
    /// Шукає об'єкти нерухомості за ключовим словом у їхній адресі або описі.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку. Не може бути порожнім або складатися з пробілів.</param>
    /// <returns>Перелік об'єктів нерухомості, що відповідають умовам.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо об'єкти не знайдені або ключове слово є некоректним.</exception>
    public IEnumerable<Property> SearchProperties(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Enumerable.Empty<Property>();

        var results = _properties.Where(p =>
            (p.Address != null && p.Address.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
            (p.Description != null && p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

        if (!results.Any())
            throw new ArgumentException($"Об'єкти нерухомості не знайдені за ключовим словом '{keyword}'.");

        return results;
    }

    /// <summary>
    /// Шукає як клієнтів, так і об'єкти нерухомості за ключовим словом.
    /// </summary>
    /// <param name="keyword">Ключове слово для пошуку. Не може бути порожнім або складатися з пробілів.</param>
    /// <returns>Перелік клієнтів і об'єктів нерухомості як об'єктів, що відповідають умовам.</returns>
    /// <exception cref="ArgumentException">Викидається, якщо результати не знайдені або ключове слово є некоректним.</exception>
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
    /// Виконує розширений пошук клієнтів на основі зазначених критеріїв.
    /// </summary>
    /// <param name="criteria">Критерії пошуку. Не можуть бути null.</param>
    /// <returns>Перелік клієнтів, що відповідають умовам.</returns>
    /// <exception cref="ArgumentNullException">Викидається, якщо критерії є null.</exception>
    /// <exception cref="ArgumentException">Викидається, якщо жоден клієнт не відповідає критеріям.</exception>
    public IEnumerable<Client> AdvancedClientSearch(ClientSearchCriteria criteria)
    {
        if (criteria == null)
            throw new ArgumentNullException(nameof(criteria));

        var results = _clients.Where(c =>
            (string.IsNullOrEmpty(criteria.FirstName) ||
                (c.FirstName != null && c.FirstName.Equals(criteria.FirstName, StringComparison.OrdinalIgnoreCase))) &&
            (string.IsNullOrEmpty(criteria.LastName) ||
                (c.LastName != null && c.LastName.Equals(criteria.LastName, StringComparison.OrdinalIgnoreCase))) &&
            (string.IsNullOrEmpty(criteria.PhoneNumber) ||
                (c.PhoneNumber != null && c.PhoneNumber.Contains(criteria.PhoneNumber, StringComparison.OrdinalIgnoreCase))) &&
            (string.IsNullOrEmpty(criteria.BankAccount) ||
                (c.BankAccount != null && c.BankAccount.Contains(criteria.BankAccount, StringComparison.OrdinalIgnoreCase))) &&
            (criteria.DesiredPropertyType == null ||
                _offers.Any(o => o.Client.Id == c.Id &&
                    o.OfferedProperties.Any(p => p.Type == criteria.DesiredPropertyType))) &&
            (criteria.Price == null ||
                _offers.Any(o => o.Client.Id == c.Id &&
                    o.OfferedProperties.Any(p => p.Price <= criteria.Price))));

        if (!results.Any())
            throw new ArgumentException("Клієнтів не знайдено за зазначеними критеріями.");

        return results;
    }
    #endregion
}