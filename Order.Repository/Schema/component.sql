-- Generated on 2026-02-27 18:17:03.038893
-- Table: component

-- Table ID: component_30
CREATE TABLE component (
cmp_id SERIAL  NOT NULL,
cmp_guid uuid  NOT NULL,
cmp_tenant_guid uuid  NOT NULL,
cmp_repo_guid uuid  NOT NULL,
cmp_component_manifest_guid uuid,
cmp_name character varying(255)  NOT NULL,
cmp_absolute_name character varying(255),
cmp_path text,
cmp_is_active boolean DEFAULT true,
cmp_is_deleted boolean DEFAULT false,
cmp_created_by character varying(100),
cmp_created_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
cmp_updated_by character varying(100),
cmp_updated_dtt timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
cmp_component_type_guid uuid,
    PRIMARY KEY (cmp_id)
);
