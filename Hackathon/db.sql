
DO $$ 
BEGIN
    IF NOT EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'user') THEN 
        CREATE TABLE public."user" (
            id int4 NOT NULL GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
            "name" varchar NOT NULL,
            email varchar NOT NULL,
            "password" varchar NOT NULL,
            CONSTRAINT user_pk PRIMARY KEY (id),
            CONSTRAINT user_un UNIQUE (email)
        );

        -- Define as permissões
        ALTER TABLE public."user" OWNER TO postgres;
        GRANT ALL ON TABLE public."user" TO postgres;
    END IF; 
END $$;



DO $$ 
BEGIN
    IF NOT EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'time_tracking') THEN 
        CREATE TABLE public.time_tracking (
		id int4 NOT NULL GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
		user_id int4 NOT NULL,
		created_at timestamp NOT NULL,
		CONSTRAINT time_tracking_pk PRIMARY KEY (id),
		CONSTRAINT time_tracking_fk FOREIGN KEY (user_id) REFERENCES public."user"(id)
	);

-- Permissions

	ALTER TABLE public.time_tracking OWNER TO postgres;
	GRANT ALL ON TABLE public.time_tracking TO postgres;
    END IF; 
END $$;



DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM public."user"
        WHERE "name" = 'Carlos Eduardo'
        AND email = 'carlos@skysoftware.com.br'
        AND "password" = '8lUR7ioJ8SUmARU8LYYIig=='
    ) THEN
        INSERT INTO public."user" ("name", email, "password") VALUES
        ('Carlos Eduardo', 'carlos@skysoftware.com.br', '8lUR7ioJ8SUmARU8LYYIig==');
    END IF;
END $$;

TRUNCATE TABLE time_tracking;

DO $$
DECLARE
  day_offset INT;
  current_day TIMESTAMP;
  morning_check_in TIME;
  afternoon_check_out TIME;
  middle_check_in TIME;
  middle_check_out TIME;
BEGIN
  FOR day_offset IN 1..59 LOOP
    current_day := CURRENT_DATE - day_offset;

    -- Pula sábados (6) e domingos (0)
    IF EXTRACT(DOW FROM current_day) = 6 OR EXTRACT(DOW FROM current_day) = 0 THEN
      CONTINUE;
    END IF;

    -- Primeiro check-in entre 08:50 e 09:15
    morning_check_in := '08:50:00'::time + (random() * (25 * 60))::int * INTERVAL '1 second';

    -- Último check-out entre 17:50 e 18:15
    afternoon_check_out := '17:50:00'::time + (random() * (25 * 60))::int * INTERVAL '1 second';

    -- Check-in intermediário entre 11:50 e 13:50, com duração entre 50 e 70 minutos
    middle_check_in := '11:50:00'::time + (random() * (120 * 60))::int * INTERVAL '1 second';
    middle_check_out := middle_check_in + (50 + random() * (20))::int * INTERVAL '1 minute';

    -- Insere o primeiro check-in
    INSERT INTO time_tracking (user_id, created_at)
    VALUES (1, current_day + morning_check_in);

    -- Insere o check-in e check-out intermediários
    INSERT INTO time_tracking (user_id, created_at)
    VALUES (1, current_day + middle_check_in);
    INSERT INTO time_tracking (user_id, created_at)
    VALUES (1, current_day + middle_check_out);

    -- Insere o último check-out
    INSERT INTO time_tracking (user_id, created_at)
    VALUES (1, current_day + afternoon_check_out);
  END LOOP;
END $$;


