﻿using PizzaStore.Models;
using System.Xml.Linq;

namespace PizzaStore.Services
{
    public class PizzaService
    {
        static List<Pizza> Pizzas { get; }
        static int nextId = 3;
        static PizzaService()
        {
            Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true },
            new Pizza { Id = 3, Name = "Peperoni", IsGlutenFree = false },
            new Pizza { Id = 4, Name = "Four Seasons", IsGlutenFree = true },
            new Pizza { Id = 5, Name = "Roman", IsGlutenFree = true },
            new Pizza { Id = 4, Name = "NewYork", IsGlutenFree = true },
            new Pizza { Id = 5, Name = "Greek", IsGlutenFree = false }
        };
        }

        public static List<Pizza> GetAll() => Pizzas;

        public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        public static void Add(Pizza pizza)
        {
            pizza.Id = nextId++;
            Pizzas.Add(pizza);
        }

        public static void Delete(int id)
        {
            var pizza = Get(id);
            if (pizza is null)
                return;

            Pizzas.Remove(pizza);
        }

        public static void Update(Pizza pizza)
        {
            var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
            if (index == -1)
                return;

            Pizzas[index] = pizza;
        }

    }
}
