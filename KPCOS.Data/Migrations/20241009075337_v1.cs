using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPCOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesignTemplate",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    default_location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    default_shape = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    default_size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    total_price = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DesignTe__3213E83F18852BEA", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    discount_percentage = table.Column<int>(type: "int", nullable: true),
                    duration = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    feature_include = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Package__3213E83F7A66ECD3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPolicy",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    payment_deadline_day = table.Column<int>(type: "int", nullable: true),
                    payment_method_available = table.Column<string>(type: "text", nullable: true),
                    refund_policy = table.Column<string>(type: "text", nullable: true),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentP__3213E83FB2EE6900", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    fullname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3213E83F3193F61C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    loyalty_point = table.Column<int>(type: "int", nullable: true),
                    membership_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    package_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    user_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3213E83FAA299BC7", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customer_Package",
                        column: x => x.package_id,
                        principalTable: "Package",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Customer_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    salary = table.Column<decimal>(type: "decimal(18,0)", nullable: true, defaultValue: 1000m),
                    supervisor_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    user_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__3213E83FEE68B788", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employee_Supervisor",
                        column: x => x.supervisor_id,
                        principalTable: "Employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Employee_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Design",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    budget = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    consultant_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    customer_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    depth = table.Column<decimal>(type: "decimal(18,0)", nullable: true, defaultValue: 0.6m),
                    design_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    filtration_system = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    koi_count_range = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    koi_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Outdoor"),
                    min_length = table.Column<decimal>(type: "decimal(18,0)", nullable: true, defaultValue: 2m),
                    min_width = table.Column<decimal>(type: "decimal(18,0)", nullable: true, defaultValue: 0.8m),
                    note = table.Column<string>(type: "text", nullable: true),
                    shape = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Rectangular"),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Not yet responded"),
                    template_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    water_level = table.Column<decimal>(type: "decimal(18,0)", nullable: true, defaultValue: 0.4m),
                    water_quality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Design__3213E83FE5726A41", x => x.id);
                    table.ForeignKey(
                        name: "FK_Design_Customer",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Design_Template",
                        column: x => x.template_id,
                        principalTable: "DesignTemplate",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceBooking",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    customer_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    service_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    booking_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    schedule_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Pending"),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ServiceB__3213E83FDB2F72FC", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceBooking_Customer",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    actual_cost = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    construction_staff_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    customer_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    designer_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    estimated_cost = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Project__3213E83F66C9E3A2", x => x.id);
                    table.ForeignKey(
                        name: "FK_Project_ConstructionStaff",
                        column: x => x.construction_staff_id,
                        principalTable: "Employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Project_Customer",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Project_Designer",
                        column: x => x.designer_id,
                        principalTable: "Employee",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Consultation",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    adjusted_design = table.Column<string>(type: "text", nullable: true),
                    adjusted_specification = table.Column<string>(type: "text", nullable: true),
                    design_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Consulta__3213E83F1A2FF1D7", x => x.id);
                    table.ForeignKey(
                        name: "FK_Consultation_Design",
                        column: x => x.design_id,
                        principalTable: "Design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Quotation",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    complexity_level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    consultation_amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    design_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    quotation_amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    quotation_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    scale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    style = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quotatio__3213E83FF9F2486B", x => x.id);
                    table.ForeignKey(
                        name: "FK_Quotation_Design",
                        column: x => x.design_id,
                        principalTable: "Design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceAssignment",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    service_booking_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    employee_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    assign_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Assigned")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ServiceA__3213E83FEBA0D20E", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceAssignment_Employee",
                        column: x => x.employee_id,
                        principalTable: "Employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ServiceAssignment_ServiceBooking",
                        column: x => x.service_booking_id,
                        principalTable: "ServiceBooking",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceExecution",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    service_booking_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    employee_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    execution_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    result = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Completed")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ServiceE__3213E83FD27E75A1", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceExecution_Employee",
                        column: x => x.employee_id,
                        principalTable: "Employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ServiceExecution_ServiceBooking",
                        column: x => x.service_booking_id,
                        principalTable: "ServiceBooking",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceFeedback",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    service_booking_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    customer_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    rating = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    feedback = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ServiceF__3213E83FE23E258A", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceFeedback_Customer",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ServiceFeedback_ServiceBooking",
                        column: x => x.service_booking_id,
                        principalTable: "ServiceBooking",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DesignConcept",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    project_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DesignCo__3213E83F11AE8A1B", x => x.id);
                    table.ForeignKey(
                        name: "FK_DesignConcept_Project",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    customer_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: true),
                    project_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    rating = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__3213E83F96D3765A", x => x.id);
                    table.ForeignKey(
                        name: "FK_Feedback_Customer",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Feedback_Project",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    discount_applied = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    payment_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    payment_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    project_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoice__3213E83F22A1D60A", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invoice_Project",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    project_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Material__3213E83F856B7201", x => x.id);
                    table.ForeignKey(
                        name: "FK_Material_Project",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_design_id",
                table: "Consultation",
                column: "design_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_package_id",
                table: "Customer",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_user_id",
                table: "Customer",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Design_customer_id",
                table: "Design",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Design_template_id",
                table: "Design",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcept_project_id",
                table: "DesignConcept",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_supervisor_id",
                table: "Employee",
                column: "supervisor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_user_id",
                table: "Employee",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_customer_id",
                table: "Feedback",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_project_id",
                table: "Feedback",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_project_id",
                table: "Invoice",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Material_project_id",
                table: "Material",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_construction_staff_id",
                table: "Project",
                column: "construction_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_customer_id",
                table: "Project",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_designer_id",
                table: "Project",
                column: "designer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_design_id",
                table: "Quotation",
                column: "design_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignment_employee_id",
                table: "ServiceAssignment",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignment_service_booking_id",
                table: "ServiceAssignment",
                column: "service_booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBooking_customer_id",
                table: "ServiceBooking",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExecution_employee_id",
                table: "ServiceExecution",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExecution_service_booking_id",
                table: "ServiceExecution",
                column: "service_booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeedback_customer_id",
                table: "ServiceFeedback",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeedback_service_booking_id",
                table: "ServiceFeedback",
                column: "service_booking_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultation");

            migrationBuilder.DropTable(
                name: "DesignConcept");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "PaymentPolicy");

            migrationBuilder.DropTable(
                name: "Quotation");

            migrationBuilder.DropTable(
                name: "ServiceAssignment");

            migrationBuilder.DropTable(
                name: "ServiceExecution");

            migrationBuilder.DropTable(
                name: "ServiceFeedback");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Design");

            migrationBuilder.DropTable(
                name: "ServiceBooking");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "DesignTemplate");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
