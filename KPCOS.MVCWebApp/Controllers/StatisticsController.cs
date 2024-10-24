using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KPCOS.Common;
using KPCOS.Data.Models;
using KPCOS.Service.Base;

public class StatisticsController : Controller
{
    // GET: Statistics
    public async Task<IActionResult> Index()
    {
        var invoices = await GetInvoices();
        var consultations = await GetConsultations();
        var customers = await GetCustomers();
        var designs = await GetDesigns();
        var designConcepts = await GetDesignConcepts();
        var designTemplates = await GetDesignTemplates();

        var viewModel = new StatisticsViewModel
        {
            Invoices = invoices,
            Consultations = consultations,
            Customers = customers,
            Designs = designs,
            DesignConcepts = designConcepts,
            DesignTemplates = designTemplates
        };

        return View(viewModel);
    }

    private async Task<List<Invoice>> GetInvoices()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Invoice"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    return JsonConvert.DeserializeObject<List<Invoice>>(result.Data.ToString());
                }
            }
        }
        return new List<Invoice>();
    }

    private async Task<List<Consultation>> GetConsultations()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Consultation"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    return JsonConvert.DeserializeObject<List<Consultation>>(result.Data.ToString());
                }
            }
        }
        return new List<Consultation>();
    }

    private async Task<List<Customer>> GetCustomers()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Customer"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    return JsonConvert.DeserializeObject<List<Customer>>(result.Data.ToString());
                }
            }
        }
        return new List<Customer>();
    }

    private async Task<List<Design>> GetDesigns()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Design"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    return JsonConvert.DeserializeObject<List<Design>>(result.Data.ToString());
                }
            }
        }
        return new List<Design>();
    }

    private async Task<List<DesignConcept>> GetDesignConcepts()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndpoint + "DesignConcept"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    return JsonConvert.DeserializeObject<List<DesignConcept>>(result.Data.ToString());
                }
            }
        }
        return new List<DesignConcept>();
    }

    private async Task<List<DesignTemplate>> GetDesignTemplates()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndpoint + "DesignTemplate"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    return JsonConvert.DeserializeObject<List<DesignTemplate>>(result.Data.ToString());
                }
            }
        }
        return new List<DesignTemplate>();
    }
}

public class StatisticsViewModel
{
    public List<Invoice> Invoices { get; set; }
    public List<Consultation> Consultations { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Design> Designs { get; set; }
    public List<DesignConcept> DesignConcepts { get; set; }
    public List<DesignTemplate> DesignTemplates { get; set; }
}