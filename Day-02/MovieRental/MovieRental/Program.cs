﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieRental
{
    public class Movie {

       public const  int  CHILDRENS = 2;
       public const  int  REGULAR = 0;
       public const  int  NEW_RELEASE = 1;

   
       private int _priceCode;
            private string _title;

            public Movie(String title, int priceCode) {
           _title = title;
           _priceCode = priceCode;
       }

					
       public int PriceCode {
           get { return _priceCode; }
           set { _priceCode = value; }
       }


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public double getAmount(int daysRented)
        {
            double thisAmount = 0;
            //determine amounts for each line
            switch (this.PriceCode)
            {
                case Movie.REGULAR:
                    thisAmount += 2;
                    if (daysRented > 2)
                        thisAmount += (daysRented - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    thisAmount += daysRented * 3;
                    break;
                case Movie.CHILDRENS:
                    thisAmount += 1.5;
                    if (daysRented > 3)
                        thisAmount += (daysRented - 3) * 1.5;
                    break;

            }
            return thisAmount;
        }

        public int getFrequentRenterPoints(int daysRented)
        {
            int frequentRenterPoints = 1;
            // add bonus for a two day new release rental
            if ((this.PriceCode == Movie.NEW_RELEASE) && daysRented > 1) ++frequentRenterPoints;
            return frequentRenterPoints;
        }
    }

    public class Rental
    {
        private Movie _movie;
        private int _daysRented;

        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }
        public int getDaysRented()
        {
            return _daysRented;
        }
        public Movie getMovie()
        {
            return _movie;
        }
        public double getAmount()
        {
            return getMovie().getAmount(this.getDaysRented());
        }

        public int getFrequentRenterPoints()
        {
            return getMovie().getFrequentRenterPoints(this.getDaysRented());
        }

    }

    public class Customer
    {
        private String _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(String name)
        {
            _name = name;
        }

    

        public void addRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public String getName()
        {
            return _name;
        }

        public String statement()
        {
            String result = "Rental Record for " + getName() + "\n";
            foreach (var rental in _rentals)
            {
                //show figures for this rental
                result += "\t" + rental.getMovie().Title + "\t" + rental.getAmount() + "\n";
            }
            //add footer lines
            result += "Amount owed is " + getTotalAmount() + "\n";
            result += "You earned " + getTotalFrequentRenterPoints() + " frequent renter points";
            return result;

        }

        private double getTotalAmount()
        {
            double totalAmount = 0;
            foreach (var rental in _rentals)
            {
                totalAmount += rental.getAmount();
            }
            return totalAmount;
        }

        private int getTotalFrequentRenterPoints()
        {
            int frequentRenterPoints = 0;
            foreach (var rental in _rentals)
            {
                // add frequent renter points
                frequentRenterPoints += rental.getFrequentRenterPoints();
            }
            return frequentRenterPoints;
        }

        

        


    
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
