using KPCOS.Data.Models;
using KPCOS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Data
{
    public class UnitOfWork
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;
        private ConsultationRepository _consultationRepository;
        private CustomerRepository _customerRepository;
        private DesignConceptRepository _designConceptRepository;
        private DesignRepository _designRepository;
        private DesignTemplateRepository _designTemplateRepository;
        private EmployeeRepository _employeeRepository;
        private FeedbackRepository _feedbackRepository;
        private InvoiceRepository _invoiceRepository;
        private MaterialRepository _materialRepository;
        private PackageRepository _packageRepository;
        private PaymentPolicyRepository _paymentPolicyRepository;
        private ProjectRepository _projectRepository;
        private QuotationRepository _quotationRepository;
        private UserRepository _userRepository;
        private ServiceAssignmentRepository _serviceAssignmentRepository;
        private ServiceBookingRepository _serviceBookingRepository;
        private ServiceExecutionRepository _serviceExecutionRepository;
        private ServiceFeedbackRepository _serviceFeedbackRepository;
        public UnitOfWork()
        {
            _context ??= new FA24_SE1717_PRN231_G4_KPCOSContext();
            _consultationRepository ??= new ConsultationRepository();
            _customerRepository ??= new CustomerRepository();
            _designConceptRepository ??= new DesignConceptRepository();
            _designRepository ??= new DesignRepository(_context);
            _designTemplateRepository ??= new DesignTemplateRepository();
            _employeeRepository ??= new EmployeeRepository();
            _feedbackRepository ??= new FeedbackRepository();
            _invoiceRepository ??= new InvoiceRepository();
            _materialRepository ??= new MaterialRepository();
            _packageRepository ??= new PackageRepository();
            _paymentPolicyRepository ??= new PaymentPolicyRepository();
            _projectRepository ??= new ProjectRepository();
            _quotationRepository ??= new QuotationRepository(_context);
            _userRepository ??= new UserRepository();
            _serviceAssignmentRepository ??= new ServiceAssignmentRepository();
            _serviceBookingRepository ??= new ServiceBookingRepository();
            _serviceExecutionRepository ??= new ServiceExecutionRepository();
            _serviceFeedbackRepository ??= new ServiceFeedbackRepository();
        }
        public ConsultationRepository Consultation
        {
            get { return _consultationRepository ??= new ConsultationRepository(_context); }
        }
        public CustomerRepository Customer
        {
            get { return _customerRepository ??= new CustomerRepository(_context); }
        }
        public DesignConceptRepository DesignConcept
        {
            get { return _designConceptRepository ??= new DesignConceptRepository(_context); }
        }
        public DesignRepository Design
        {
            get { return _designRepository ??= new DesignRepository(_context); }
        }
        public DesignTemplateRepository DesignTemplate
        {
            get { return _designTemplateRepository ??= new DesignTemplateRepository(_context); }
        }
        public EmployeeRepository Employee
        {
            get { return _employeeRepository ??= new EmployeeRepository(_context); }
        }
        public FeedbackRepository Feedback
        {
            get { return _feedbackRepository ??= new FeedbackRepository(_context); }
        }
        public InvoiceRepository Invoice
        {
            get { return _invoiceRepository ??= new InvoiceRepository(_context); }
        }
        public MaterialRepository Material
        {
            get { return _materialRepository ??= new MaterialRepository(_context); }
        }
        public PackageRepository Package
        {
            get { return _packageRepository ??= new PackageRepository(_context); }
        }
        public PaymentPolicyRepository PaymentPolicy
        {
            get { return _paymentPolicyRepository ??= new PaymentPolicyRepository(_context); }
        }
        public ProjectRepository Project
        {
            get { return _projectRepository ??= new ProjectRepository(_context); }
        }
        public QuotationRepository Quotation
        {
            get { return _quotationRepository ??= new QuotationRepository(_context); }
        }
        public UserRepository User
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }
        public ServiceAssignmentRepository ServiceAssignment
        {
            get { return _serviceAssignmentRepository ??= new ServiceAssignmentRepository(_context); }
        }
        public ServiceBookingRepository ServiceBooking
        {
            get { return _serviceBookingRepository ??= new ServiceBookingRepository(_context); }
        }
        public ServiceExecutionRepository ServiceExecution
        {
            get { return _serviceExecutionRepository ??= new ServiceExecutionRepository(_context); }
        }
        public ServiceFeedbackRepository ServiceFeedback
        {
            get { return _serviceFeedbackRepository ??= new ServiceFeedbackRepository(_context); }
        }


        //Nang cao de di lam thuc te

        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
        #endregion
    }
}
