using Expense_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Expense_tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseTrackerDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpenseTrackerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();

            var totalExpenses = _context.Expenses.Sum(expense => expense.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }

        public IActionResult CreateAndEditExpense(int? Id)
        {
            if (Id != null)
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == Id);
                return View(expenseInDb);
            }

            return View();
        }

        public IActionResult DeleteExpense(int Id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == Id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult RedirectEdit(Expense expense)
        {
            if (expense.Id == 0)
            {
                _context.Expenses.Add(expense);
            }
            else
            {
                _context.Expenses.Update(expense);
            }

            _context.SaveChanges();

            return RedirectToAction("Expenses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
