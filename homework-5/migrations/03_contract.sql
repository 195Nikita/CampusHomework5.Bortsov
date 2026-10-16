-- CAMPUS-102, phase 3: Contract only after every instance uses CapacityInteger.
BEGIN;
SET LOCAL lock_timeout = '5s';

LOCK TABLE "Auditoriums" IN ACCESS EXCLUSIVE MODE;

ALTER TABLE "Auditoriums"
    ALTER COLUMN "CapacityInteger" SET NOT NULL;

DROP TRIGGER IF EXISTS "TR_Auditoriums_CapacityInteger" ON "Auditoriums";
DROP FUNCTION IF EXISTS sync_auditorium_capacity_integer();

ALTER TABLE "Auditoriums" DROP COLUMN "Capacity";
ALTER TABLE "Auditoriums" RENAME COLUMN "CapacityInteger" TO "Capacity";
ALTER TABLE "Auditoriums"
    RENAME CONSTRAINT "CK_Auditoriums_CapacityInteger_NonNegative"
    TO "CK_Auditoriums_Capacity_NonNegative";

COMMIT;
