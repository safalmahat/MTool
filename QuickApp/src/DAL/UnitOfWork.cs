﻿// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        ICustomerRepository _customers;
        IProductRepository _products;
        IOrdersRepository _orders;
        IStudentRepository _students;
        IChannelRepository _channel;
        IEnquiryListRepository _enquiryList;
        IMarketingStudentRepository _marketingStudentList;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }



        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }

        public IStudentRepository Students
        {
            get
            {
                if (_students == null)
                    _students = new StudentRepository(_context);

                return _students;
            }
        }



        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }



        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }
        public IChannelRepository Channel
        {
            get
            {
                if (_channel == null)
                    _channel = new ChannelRepository(_context);

                return _channel;
            }
                
        }

        public IEnquiryListRepository EnquiryList
        {
            get
            {
                if (_enquiryList == null)
                    _enquiryList = new EnquiryListRepository(_context);

                return _enquiryList;
            }

        }

        public IMarketingStudentRepository MarketingStudentList
        {
            get
            {
                if (_marketingStudentList == null)
                    _marketingStudentList = new MarketingStudentRepository(_context);

                return _marketingStudentList;
            }

        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
