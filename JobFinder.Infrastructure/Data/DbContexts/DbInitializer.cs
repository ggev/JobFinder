using JobFinder.Domain.Entities;
using JobFinder.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Data.DbContexts
{
    public static class DbInitializer
    {
        public static async Task Seed()
        {
            await using var context = new SqlDbContext(new DbContextOptions<SqlDbContext>());
            if (!await context.Set<Category>().AnyAsync() && !await context.Set<Job>().AnyAsync())
            {
                var category1 = new Category { Name = "Information Technology" };
                var category2 = new Category { Name = "Sales" };
                var category3 = new Category { Name = "Management" };
                var category4 = new Category { Name = "Engineering, Technology" };
                var category5 = new Category { Name = "Advertising, Marketing, PR" };
                var category6 = new Category { Name = "Retail" };
                var category7 = new Category { Name = "Logistics, Transportation" };
                var category8 = new Category { Name = "Architecture, Construction" };
                var category9 = new Category { Name = "Security" };
                var category10 = new Category { Name = "HR, Recruiting, Staffing" };
                var category11 = new Category { Name = "Other" };

                await context.Set<Category>().AddRangeAsync(category1, category2, category3, category4, category5,
                    category6, category7, category8, category9, category10, category11);

                var company1 = new Company { Name = "Apple", Logo = "apple.png", Location = City.Abovyan, Address = "Suren Mnatsakanyan 62" };
                var company2 = new Company { Name = "Dell", Logo = "dell.jpg", Location = City.Armavir, Address = "Sayat Nova 5" };
                var company3 = new Company { Name = "Google", Logo = "google.png", Location = City.Gyumri, Address = "Minas Avetisyan 9/22" };
                var company4 = new Company { Name = "Hp", Logo = "hp.jpg", Location = City.Hrazdan, Address = "Zoravar Andranik 3" };
                var company5 = new Company { Name = "Tesla", Logo = "tesla.png", Location = City.Yerevan, Address = "Sebastia 43" };
                var company6 = new Company { Name = "Toshiba", Logo = "toshiba.png", Location = City.Kapan, Address = "Melik Stepanyan 4" };

                await context.Set<Company>().AddRangeAsync(company1, company2, company3, company4, company5, company6);

                var job1 = new Job
                {
                    Company = company1,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-3),
                    Title = "IS&T Software Engineer",
                    Description = "The Information System Engineer will work with IS&T team (Information Systems & Technology) and regional/local business users to implement strategic supply chain solutions at Apple. " +
                                  "The person should have good technical knowledge of develop application with basic domain knowledge in procurement and supply chain planning.As a member of the SCI team(Supply Chain Integration) in China," +
                                  "you will be responsible for requirement analysis, implement technical solutions, testing and support of SCI applications.You will also contribute in high level design discussions with global " +
                                  "IS & T development team regarding application framework and architecture.You need to have expertise to develop and implement applications as per business requirements within strict timelines, " +
                                  "with proven experience in building enterprise applications that can support high transactional volumes in a highly concurrent environment."
                };
                var job2 = new Job
                {
                    Company = company1,
                    EmploymentType = EmploymentType.FullTime,
                    CreatedDt = DateTime.UtcNow.AddDays(-4),
                    Title = "Training Manager - Retail Contact Centre",
                    Description = "We have an open position for an enthusiastic, motivated training manager to join our team. You will be responsible for assisting " +
                                  "the Regional Learning Leader in coordinating internal and external learning activities, negotiating and allocating training resources, and conducting training review through data analysis and feedback."
                };
                var job3 = new Job
                {
                    Company = company1,
                    EmploymentType = EmploymentType.Seasonal,
                    CreatedDt = DateTime.UtcNow.AddDays(-2),
                    Title = "QA Engineer",
                    Description = "The European Localisation Quality team is responsible for designing, implementing and executing both automated and manual test plans in an effort to assure the quality of " +
                                  "Apple’s localised products. As a QA Engineer in the ELQ organisation you will support the lifecycle of various Apple software products from product introduction to release. " +
                                  "While coordinating testing you’ll also collaborate with international software QA centres, software engineering teams, translators and project managers to ensure that " +
                                  "Apple delivers world-class software products to our international markets."
                };
                var job4 = new Job
                {
                    Company = company2,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-5),
                    Title = "Senior Data Engineer – Cloud Solution Architect",
                    Description = "People make Dell – so wherever in the world they work, everyone is rewarded for their contribution. We can’t wait for you to find that out yourself! " +
                                  "The Dell Technologies family are looking for a Senior Data Engineer – Cloud Solution Architect to join our Product Development team in Cork, Ireland."
                };
                var job5 = new Job
                {
                    Company = company2,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-1),
                    Title = "Open RAN Developer/SME",
                    Description = "Our engineering team is responsible for the creation and delivery of great software products and solutions, as well as services offerings for and to this market. " +
                                  "The team works closely with a world-class product management team in defining and road-mapping the offerings. Together, we have a strong track record of " +
                                  "creating unique value for Dell Technologies, with a strong, positive impact on the company’s bottom line. The engineering team works closely with key service providers " +
                                  "customers around the world. Collaboratively, we create production-worthy offerings that are unique, cutting-edge, and shape this emerging market. With each success we achieve, " +
                                  "we push the envelope more for our customers, and then rinse and repeat!"
                };
                var job6 = new Job
                {
                    Company = company3,
                    EmploymentType = EmploymentType.FullTime,
                    CreatedDt = DateTime.UtcNow.AddDays(-3),
                    Title = "Partner Operations Manager, People Operations",
                    Description = "Google is known for our innovative technologies, products and services — and for the people behind them. The Google People Services (GPS) team focuses on " +
                                  "providing an amazing experience to past, present and future Googlers. As a Project Manager, you will create and drive the programs that help us to find and develop talent. " +
                                  "You will oversee the projects that allow GPS to provide candidates and clients with an excellent experience as productively and efficiently as possible. " +
                                  "Using your communication skills, you will set team strategy, influence business partners and facilitate change management across the department. " +
                                  "You'll partner with other project managers, client-group managers and engineers to set strategy, build support processes and develop content that delights our users."
                };
                var job7 = new Job
                {
                    Company = company3,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-4),
                    Title = "Sales Specialist, Google Workspace",
                    Description = "As a member of the Google Cloud team, you inspire leading companies, schools, and government agencies to work smarter with Google tools like Google Workspace, Search, " +
                                  "and Chrome. You advocate the innovative power of our products to make organizations more productive, collaborative, and mobile. Your guiding light is doing what’s right " +
                                  "for the customer, you will meet customers exactly where they are at and provide them the best solutions for innovation. Using your passion for Google products, " +
                                  "you help spread the magic of Google to organizations around the world."
                };
                var job8 = new Job
                {
                    Company = company3,
                    EmploymentType = EmploymentType.Seasonal,
                    CreatedDt = DateTime.UtcNow.AddDays(-7),
                    Title = "Software Engineering Manager, Data Processing",
                    Description = "Like Google's own ambitions, the work of a Software Engineer (SWE) goes way beyond just Search. SWE Managers have not only the technical chops to roll " +
                                  "up their sleeves and provide technical leadership to major projects, but also manage a team of engineers. You not only optimize your own code but make " +
                                  "sure engineers are able to optimize theirs. As a SWE Manager you manage your project goals, contribute to product strategy and help develop your team. " +
                                  "SWE teams work all across the company, in areas such as information retrieval, artificial intelligence, natural language processing, distributed computing, " +
                                  "large-scale system design, networking, security, data compression, user interface design; the list goes on and is growing every day. Operating with scale " +
                                  "and speed, our world-class software engineers are just getting started -- and as a manager, you guide the way."
                };
                var job9 = new Job
                {
                    Company = company4,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-3),
                    Title = "Packaging Engineering Manager",
                    Description = "Are you a self-driven person looking to advance your career as a high-impact player on a team? If so, we have an exciting challenge for you and your future! " +
                                  "HP Hood’s culture is built on value commitments to innovation, quality, results, integrity, community, people, and collaboration that fosters a strong employee " +
                                  "engagement, teamwork, safety and wellness. We offer a competitive benefits package that includes health, dental, vision, wellness programs, employee discounts, " +
                                  "401k matches, tuition reimbursement, ongoing development, advancement opportunities and more.This position is also eligible for our bonus program."
                };
                var job10 = new Job
                {
                    Company = company4,
                    EmploymentType = EmploymentType.FullTime,
                    CreatedDt = DateTime.UtcNow.AddDays(-5),
                    Title = "Workstations Tech Marketing Engineer",
                    Description = "HP Workstations is looking for a talented and energetic engineer to join the Workstations Technical Marketing team. In this role, you’ll have the opportunity " +
                                  "to work with the latest and greatest technology supporting our newest workstation products. The role of a Technical Marketing Engineer is multi-faceted, and " +
                                  "works with nearly all of the different functions within HP including R&D, Marketing, Sales, and often directly with our customers. Engineers on this team do a " +
                                  "mix of activities working with our application providers, working with our component partners, analyzing application performance, supporting our field teams, " +
                                  "developing deep expertise in various topics impacting our Workstation products in how they perform, and become a knowledge base for key applications and technical " +
                                  "tends used by our customers providing feedback to key component suppliers such as CPU, GPU, and storage devices."
                };
                var job11 = new Job
                {
                    Company = company4,
                    EmploymentType = EmploymentType.PartTime,
                    CreatedDt = DateTime.UtcNow.AddDays(-1),
                    Title = "Firmware Development Engineer",
                    Description = "HP is seeking an experienced firmware development engineer to join its LaserJet printer R&D team in beautiful Boise, Idaho! As a firmware engineer in Boise, " +
                                  "you’ll join a high energy development team creating innovative solutions that are disrupting the print and copier markets. HP, a worldwide leader in computing " +
                                  "and print technology, is a fortune 50 company with sites around the world. In addition to working with some of the most talented engineers in the industry, HP " +
                                  "provides a wealth of benefits and opportunities."
                };
                var job12 = new Job
                {
                    Company = company6,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-2),
                    Title = "Systems Engineer",
                    Description = "Toshiba America Business Solutions, a leader in digital technology, is seeking a Systems Engineer, to support solutions within Professional Services in " +
                                  "the Michigan marketplace. Toshiba is an industry leader in equipment, digital displays, document security, and software solutions that keep businesses " +
                                  "running brilliantly and efficiently. Our people bring innovative, real - world solutions for our client's print management needs; we help cut costs, secure " +
                                  "documents, and reduce the environmental footprint. We are a growing, dynamic organization that has a need for someone to contribute their professional best."
                };
                var job13 = new Job
                {
                    Company = company6,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-8),
                    Title = "Inside Sales Representative",
                    Description = "Toshiba America Business Solutions, a leader in digital technology, is seeking an Inside Sales Representative, to support the Louisville, KY marketplace. " +
                                  "Toshiba is an industry leader in equipment, digital displays, document security, and software solutions that keep businesses running brilliantly and efficiently. " +
                                  "Our people bring innovative, real - world solutions for our client's print management needs; we help cut costs, secure documents, and reduce the environmental " +
                                  "footprint. We are a growing, dynamic organization that has a need for someone to contribute their professional best."
                };
                var job14 = new Job
                {
                    Company = company5,
                    EmploymentType = EmploymentType.Contractor,
                    CreatedDt = DateTime.UtcNow.AddDays(-1),
                    Title = "Energy Customer Support Specialist - Remote",
                    Description = "Tesla Energy Support Specialists handle a variety of customer issues while delivering on world class customer service. The role of a specialist is to resolve " +
                            "or escalate complaints through appropriate channels and address social media escalations directed at the CEO with critical thinking. Deliver on Tesla Measures " +
                            "of Excellence, perform other duties and assignments including administrative, special projects. Support general call center functions that reinforce the mission " +
                            "to accelerate the world’s transition to sustainable energy."
                };
                var job15 = new Job
                {
                    Company = company5,
                    EmploymentType = EmploymentType.FullTime,
                    CreatedDt = DateTime.UtcNow.AddDays(-3),
                    Title = "Tesla Advisor",
                    Description = "As a Tesla Advisor it is your responsibility to offer seamless customer service throughout the entire customer journey. You have relentless determination " +
                                  "to succeed and you are driven by the mission of Tesla to accelerate the world’s transition to sustainable energy. You act as a pioneer for Tesla products, " +
                                  "sharing your knowledge and building lasting relationships."
                };

                await context.Set<Job>().AddRangeAsync(job1, job2, job3, job4, job5, job6, job7, job8, job9, job10, job11,
                    job12, job13, job14, job15);

                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job1, Category = category4 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job2, Category = category3 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job3, Category = category1 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job4, Category = category1 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job5, Category = category1 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job6, Category = category3 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job7, Category = category2 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job8, Category = category3 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job9, Category = category4 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job10, Category = category4 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job11, Category = category1 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job12, Category = category4 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job13, Category = category2 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job14, Category = category11 });
                await context.Set<JobCategory>().AddAsync(new JobCategory { Job = job15, Category = category5 });

                await context.SaveChangesAsync();
            }
        }
    }
}