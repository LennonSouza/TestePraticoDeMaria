CREATE OR REPLACE FUNCTION fn_auditoria()
RETURNS TRIGGER AS $body$
DECLARE
    v_snapshot TEXT;
    v_id       INTEGER;
BEGIN
    IF TG_OP = 'DELETE' THEN
        v_snapshot := row_to_json(OLD)::TEXT;
        v_id       := OLD.id;
        INSERT INTO auditorias
            (entidade, id_registro, operacao, data_hora, usuario, snapshot_json)
        VALUES
            (TG_TABLE_NAME, v_id, TG_OP, NOW(), current_user, v_snapshot);
        RETURN OLD;
    ELSE
        v_snapshot := row_to_json(NEW)::TEXT;
        v_id       := NEW.id;
        INSERT INTO auditorias
            (entidade, id_registro, operacao, data_hora, usuario, snapshot_json)
        VALUES
            (TG_TABLE_NAME, v_id, TG_OP, NOW(), current_user, v_snapshot);
        RETURN NEW;
    END IF;
END;
$body$ LANGUAGE plpgsql;

CREATE TRIGGER trg_auditoria_clientes
AFTER INSERT OR UPDATE OR DELETE ON clientes
FOR EACH ROW EXECUTE FUNCTION fn_auditoria();

CREATE TRIGGER trg_auditoria_servicos
AFTER INSERT OR UPDATE OR DELETE ON servicos
FOR EACH ROW EXECUTE FUNCTION fn_auditoria();

CREATE TRIGGER trg_auditoria_ordens_servico
AFTER INSERT OR UPDATE OR DELETE ON ordens_servico
FOR EACH ROW EXECUTE FUNCTION fn_auditoria();

CREATE TRIGGER trg_auditoria_itens
AFTER INSERT OR UPDATE OR DELETE ON ordens_servico_itens
FOR EACH ROW EXECUTE FUNCTION fn_auditoria();