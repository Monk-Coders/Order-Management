--Database Postgres
-- Table: Order

-- Table ID: order
CREATE TABLE IF NOT EXISTS order (
ord_id SERIAL  NOT NULL,
ord_guid uuid  NOT NULL,
ord_customer_id uuid  NOT NULL,
ord_ordered_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
ord_total_amount numeric(18,2)  NOT NULL,
ord_item_count integer  NOT NULL,
ord_status uuid,
ord_is_active boolean DEFAULT true,
ord_is_deleted boolean DEFAULT false,
ord_created_by character varying(100),
ord_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
ord_modified_by character varying(100),
ord_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (ord_id)
);

-- Table ID : order_item
CREATE TABLE IF NOT EXISTS order_item (
oit_id SERIAL  NOT NULL,
oit_guid uuid  NOT NULL,
oit_order_guid uuid  NOT NULL,
oit_product_id uuid  NOT NULL,
oit_quantity integer  NOT NULL,
oit_unit_price numeric(18,2)  NOT NULL,
oit_total_price numeric(18,2)  NOT NULL,
oit_is_active boolean DEFAULT true,
oit_is_deleted boolean DEFAULT false,
oit_created_by character varying(100),
oit_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
oit_modified_by character varying(100),
oit_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (oit_id)
);

-- Table ID : order_status
CREATE TABLE IF NOT EXISTS order_status (
ost_id SERIAL  NOT NULL,
ost_guid uuid  NOT NULL,
ost_name character varying(255)  NOT NULL,
ost_description text,
ost_is_active boolean DEFAULT true,
ost_is_deleted boolean DEFAULT false,
ost_created_by character varying(100),
ost_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
ost_modified_by character varying(100),
ost_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (ost_id)
);

--Table ID : Product
CREATE TABLE IF NOT EXISTS product (
prd_id SERIAL  NOT NULL,
prd_guid uuid  NOT NULL,
prd_name character varying(255)  NOT NULL,
prd_description text,
prd_code character varying(100)  NOT NULL,
prd_category UUID,
prd_price numeric(18,2)  NOT NULL,
prd_is_active boolean DEFAULT true,
prd_is_deleted boolean DEFAULT false,
prd_created_by character varying(100),
prd_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
prd_modified_by character varying(100),
prd_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (prd_id)
);

--Table ID : Product Pricing

CREATE TABLE IF NOT EXISTS product_pricing (
pp_id SERIAL  NOT NULL,
pp_guid uuid  NOT NULL,
pp_product_guid uuid  NOT NULL,
pp_price numeric(18,2)  NOT NULL,
pp_effective_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
pp_expiry_date timestamp without time zone,
pp_is_active boolean DEFAULT true,
pp_is_deleted boolean DEFAULT false,
pp_created_by character varying(100),
pp_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
pp_modified_by character varying(100),
pp_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (pp_id)
);


--Order ID,Product ID,Customer ID,Product Name,Category,Region,Date of Sale,Quantity Sold,Unit Price,Discount,Shipping Cost,Payment Method,Customer Name,Customer Email,Customer Address

Table ID : Customer
CREATE TABLE IF NOT EXISTS customer (
cst_id SERIAL  NOT NULL,
cst_guid uuid  NOT NULL,
cst_name character varying(255)  NOT NULL,
cst_email character varying(255)  NOT NULL,
cst_address text,
cst_is_active boolean DEFAULT true,
cst_is_deleted boolean DEFAULT false,
cst_created_by character varying(100),
cst_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
cst_modified_by character varying(100),
cst_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (cst_id)
);

-- Table ID : Invoice
CREATE TABLE IF NOT EXISTS invoice (
inv_id SERIAL  NOT NULL,
inv_guid uuid  NOT NULL,
inv_order_guid uuid  NOT NULL,
inv_invoice_number character varying(100)  NOT NULL,
inv_invoice_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
inv_payment_method character varying(100),
inv_billing_address text,
inv_shipping_address text,
inv_discount_amount numeric(18,2)  NOT NULL,
inv_total_amount numeric(18,2)  NOT NULL,
inv_due_date timestamp without time zone,
inv_is_active boolean DEFAULT true,
inv_is_deleted boolean DEFAULT false,
inv_created_by character varying(100),
inv_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
inv_modified_by character varying(100),
inv_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (inv_id)
);

create table if not exists product_category (
prd_id SERIAL  NOT NULL,
prd_guid UUID,
prd_name character varying(255)  NOT NULL,
prd_description text,
prd_created_by character varying(100),
prd_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
prd_modified_by character varying(100),
prd_modified_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (prd_id)
);

create index idx_order_guid on order (ord_guid);
create index idx_order_item_guid on order_item (oit_guid);
create index idx_order_item_order_guid on order_item (oit_order_guid);
create index idx_order_status_guid on order_status (ost_guid);
create index idx_product_guid on product (prd_guid);
create index idx_product_pricing_guid on product_pricing (pp_guid);
create index idx_product_pricing_product_guid on product_pricing (pp_product_guid);
create index idx_customer_guid on customer (cst_guid);
create index idx_invoice_guid on invoice (inv_guid);



