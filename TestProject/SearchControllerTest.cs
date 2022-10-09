using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using NUnit.Framework;
using SearchAPI.Controllers;

namespace TestProject;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        // Arrange
        var controller = new SearchController();

        // Act
        string json = await controller.SearchByQuery("experience", 10);
        var result = JsonConvert.DeserializeObject<SearchResult>(json) ?? new SearchResult();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(1, result?.Hits);
    }
}