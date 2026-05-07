-- ------------------------------------------------------------
-- Tabela: clientes
-- ------------------------------------------------------------
CREATE TABLE clientes (
    id            SERIAL      PRIMARY KEY,
    nome          VARCHAR(150) NOT NULL,
    documento     VARCHAR(18)  NOT NULL,
    tipo          SMALLINT     NOT NULL,
    email         VARCHAR(150),
    telefone      VARCHAR(20),
    data_cadastro TIMESTAMP    NOT NULL DEFAULT NOW(),
    ativo         BOOLEAN      NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_clientes_documento UNIQUE (documento),
    CONSTRAINT ck_clientes_tipo      CHECK  (tipo IN (0, 1)),
    CONSTRAINT ck_cliente_nome       CHECK (LENGTH(TRIM(nome)) > 0)
);

CREATE INDEX idx_clientes_documento ON clientes (documento);
CREATE INDEX idx_clientes_ativo     ON clientes (ativo);

-- ------------------------------------------------------------
-- Tabela: servicos
-- ------------------------------------------------------------
CREATE TABLE servicos (
    id                 SERIAL         PRIMARY KEY,
    nome               VARCHAR(150)   NOT NULL,
    valor_base         NUMERIC(18, 2) NOT NULL,
    percentual_imposto NUMERIC(5, 2)  NOT NULL DEFAULT 0,
    ativo              BOOLEAN        NOT NULL DEFAULT TRUE,

    CONSTRAINT ck_servico_nome                CHECK (LENGTH(TRIM(nome)) > 0),
    CONSTRAINT ck_servicos_valor_base         CHECK (valor_base > 0),
    CONSTRAINT ck_servicos_percentual_imposto CHECK (percentual_imposto >= 0 AND percentual_imposto <= 100)
);

CREATE INDEX idx_servicos_ativo ON servicos (ativo);

-- ------------------------------------------------------------
-- Tabela: ordens_servico
-- ------------------------------------------------------------
CREATE TABLE ordens_servico (
    id             SERIAL         PRIMARY KEY,
    cliente_id     INTEGER        NOT NULL,
    data_abertura  TIMESTAMP      NOT NULL DEFAULT NOW(),
    data_conclusao TIMESTAMP,
    status         SMALLINT       NOT NULL DEFAULT 1,
    observacao     TEXT,
    valor_total    NUMERIC(18, 2) NOT NULL DEFAULT 0,
    versao         INTEGER        NOT NULL DEFAULT 0,

    CONSTRAINT fk_os_cliente FOREIGN KEY (cliente_id)
        REFERENCES clientes (id),

    CONSTRAINT ck_os_status CHECK (status IN (1, 2, 3, 4)),
    CONSTRAINT ck_os_valor_total CHECK (valor_total >= 0)
);

CREATE INDEX idx_os_cliente_id    ON ordens_servico (cliente_id);
CREATE INDEX idx_os_data_abertura ON ordens_servico (data_abertura);
CREATE INDEX idx_os_status        ON ordens_servico (status);

-- Partial index — OS abertas (as mais consultadas)
CREATE INDEX idx_os_abertas ON ordens_servico (data_abertura)
    WHERE status IN (1, 2);

-- ------------------------------------------------------------
-- Tabela: ordens_servico_itens
-- ------------------------------------------------------------
CREATE TABLE ordens_servico_itens (
    id                        SERIAL         PRIMARY KEY,
    ordem_servico_id          INTEGER        NOT NULL,
    servico_id                INTEGER        NOT NULL,
    quantidade                INTEGER        NOT NULL,
    valor_unitario            NUMERIC(18, 2) NOT NULL,
    percentual_imposto_aplicado NUMERIC(5, 2) NOT NULL DEFAULT 0,
    valor_total_item          NUMERIC(18, 2) NOT NULL,

  CONSTRAINT fk_item_os FOREIGN KEY (ordem_servico_id)
        REFERENCES ordens_servico(id) ON DELETE CASCADE,
    CONSTRAINT fk_item_servico FOREIGN KEY (servico_id)
        REFERENCES servicos (id),

    CONSTRAINT ck_item_quantidade    CHECK (quantidade > 0),
    CONSTRAINT ck_item_valor_total   CHECK (valor_total_item >= 0),
    CONSTRAINT ck_item_pct_imposto   CHECK (percentual_imposto_aplicado >= 0 AND percentual_imposto_aplicado <= 100)
);

CREATE INDEX idx_itens_os_id ON ordens_servico_itens (ordem_servico_id);

-- ------------------------------------------------------------
-- Tabela: historico_status
-- ------------------------------------------------------------
CREATE TABLE historico_status (
    id               SERIAL    PRIMARY KEY,
    ordem_servico_id INTEGER   NOT NULL,
    status_anterior  SMALLINT  NOT NULL,
    status_novo      SMALLINT  NOT NULL,
    data_hora        TIMESTAMP NOT NULL DEFAULT NOW(),
    usuario          VARCHAR(100) NOT NULL,
    observacao       TEXT,

    CONSTRAINT fk_hist_os FOREIGN KEY (ordem_servico_id)
        REFERENCES ordens_servico (id),

    CONSTRAINT ck_hist_status_anterior CHECK (status_anterior IN (1, 2, 3, 4)),
    CONSTRAINT ck_hist_status_novo     CHECK (status_novo     IN (1, 2, 3, 4))
);

CREATE INDEX idx_hist_os_id ON historico_status (ordem_servico_id);

-- ------------------------------------------------------------
-- Tabela: auditorias
-- ------------------------------------------------------------
CREATE TABLE auditorias (
    id            SERIAL       PRIMARY KEY,
    entidade      VARCHAR(50)  NOT NULL,
    id_registro   INTEGER      NOT NULL,
    operacao      VARCHAR(10)  NOT NULL,
    data_hora     TIMESTAMP    NOT NULL DEFAULT NOW(),
    usuario       VARCHAR(100) NOT NULL,
    snapshot_json TEXT         NOT NULL,

    CONSTRAINT ck_auditoria_operacao CHECK (operacao IN ('INSERT', 'UPDATE', 'DELETE'))
);

CREATE INDEX idx_auditoria_entidade    ON auditorias (entidade, id_registro);
CREATE INDEX idx_auditoria_data_hora   ON auditorias (data_hora);

-- ------------------------------------------------------------
-- Dados iniciais — serviços de exemplo
-- ------------------------------------------------------------
INSERT INTO servicos (nome, valor_base, percentual_imposto) VALUES
    ('Consultoria técnica',    250.00, 8.00),
    ('Instalação de rede',     800.00, 8.00),
    ('Suporte remoto',         120.00, 5.00),
    ('Desenvolvimento de software', 1500.00, 8.00),
    ('Treinamento',            400.00, 0.00);

-- ------------------------------------------------------------
-- Triggers de auditoria para clientes, serviços, ordens de serviço e itens
-- ------------------------------------------------------------
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