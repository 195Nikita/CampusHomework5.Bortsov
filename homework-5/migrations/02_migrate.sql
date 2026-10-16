-- CAMPUS-102, phase 2: Migrate historical rows while both columns exist.
BEGIN;
SET LOCAL lock_timeout = '5s';
SET LOCAL statement_timeout = '2min';

UPDATE "Auditoriums"
SET "CapacityInteger" = "Capacity"::integer
WHERE "CapacityInteger" IS NULL
   OR "CapacityInteger" IS DISTINCT FROM "Capacity"::integer;

ALTER TABLE "Auditoriums"
    VALIDATE CONSTRAINT "CK_Auditoriums_CapacityInteger_NonNegative";

DO $$
BEGIN
    IF EXISTS (
        SELECT 1 FROM "Auditoriums"
        WHERE "CapacityInteger" IS NULL
           OR "CapacityInteger" IS DISTINCT FROM "Capacity"::integer
    ) THEN
        RAISE EXCEPTION 'Capacity migration verification failed';
    END IF;
END;
$$;

COMMIT;
