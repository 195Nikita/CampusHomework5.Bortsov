-- CAMPUS-102, phase 1: Expand. Safe while the old application is running.
BEGIN;
SET LOCAL lock_timeout = '5s';

ALTER TABLE "Auditoriums"
    ADD COLUMN IF NOT EXISTS "CapacityInteger" integer;

ALTER TABLE "Auditoriums"
    ADD CONSTRAINT "CK_Auditoriums_CapacityInteger_NonNegative"
    CHECK ("CapacityInteger" IS NULL OR "CapacityInteger" >= 0)
    NOT VALID;

-- Compatibility for an old service instance that still writes only Capacity.
CREATE OR REPLACE FUNCTION sync_auditorium_capacity_integer()
RETURNS trigger
LANGUAGE plpgsql
AS $$
BEGIN
    NEW."CapacityInteger" := COALESCE(NEW."CapacityInteger", NEW."Capacity"::integer);
    RETURN NEW;
END;
$$;

CREATE TRIGGER "TR_Auditoriums_CapacityInteger"
BEFORE INSERT OR UPDATE OF "Capacity", "CapacityInteger" ON "Auditoriums"
FOR EACH ROW EXECUTE FUNCTION sync_auditorium_capacity_integer();

COMMIT;
