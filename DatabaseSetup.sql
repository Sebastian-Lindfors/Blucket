-- Ta bort databasen

REVOKE CONNECT ON DATABASE blucket FROM PUBLIC;
SELECT pg_terminate_backend(pg_stat_activity.pid)
FROM pg_stat_activity
WHERE pg_stat_activity.datname = 'blucket'
AND pid <> pg_backend_pid();

-- Måste köras separat
DROP DATABASE blucket;

-- Ta bort användare

REVOKE ALL PRIVILEGES ON SCHEMA public FROM blucket_all_access;
ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE ALL ON TABLES FROM blucket_all_access;
ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE ALL ON SEQUENCES FROM blucket_all_access;
ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE ALL ON FUNCTIONS FROM blucket_all_access;

DROP USER blucket_all_access;

-- Skapa databas

CREATE DATABASE blucket;

CREATE USER blucket_all_access WITH PASSWORD 'strong_password';

GRANT ALL PRIVILEGES ON DATABASE blucket TO blucket_all_access;

-- Anslut till den nya databasen

-- Bevilja alla rättigheter på schemat public till användaren
GRANT ALL PRIVILEGES ON SCHEMA public TO blucket_all_access;

-- Bevilja rättigheter på framtida tabeller, sekvenser och funktioner i schemat public
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO blucket_all_access;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO blucket_all_access;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON FUNCTIONS TO blucket_all_access;

-- Bevilja specifika rättigheter på alla nuvarande tabeller, sekvenser och funktioner i schemat public
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO blucket_all_access;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO blucket_all_access;
GRANT ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA public TO blucket_all_access;
