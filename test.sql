create table file
(
   id_file      timeuuid primary key,
   "Extension"  varchar,
   "File_name"  varchar,
   "File_path"  varchar,
   "Size_in_kb" float
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';


create table image
(
   id_image     timeuuid primary key,
   "Name"       varchar,
   "Path"       varchar,
   "Size_in_kb" float
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';



create table invoice
(
   id_invoice timeuuid primary key,
   "Amount"   float,
   id_file    timeuuid,
   id_order   timeuuid
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';



create table notification
(
   id_notification timeuuid primary key,
   "Is_read"       boolean,
   "Message"       varchar,
   id_user         timeuuid
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';



create table "order"
(
   id_order  timeuuid primary key,
   "Amount"  float,
   "Paid_At" timestamp
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';


create table project
(
   id_project timeuuid primary key,
   "Name"     varchar,
   "Status"   varchar,
   id_image   timeuuid
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table project_files
(
   id_project   timeuuid,
   id_files     timeuuid,
   extension    varchar,
   file_name    varchar,
   project_name varchar,
   size_in_kb   varchar,
   primary key (id_project, id_files)
)
   with comment = 'C4: Mostrar lista de archivos por proyecto'
    and caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table project_messages
(
   id_project      timeuuid,
   id_notification timeuuid,
   content         varchar,
   project_name    varchar,
   primary key (id_project, id_notification)
)
   with comment = 'C3 = Mostrar lista de mensaje por proyecto'
    and caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table project_users
(
   id_project       timeuuid,
   id_user          timeuuid,
   email            varchar,
   project_name     varchar,
   "user_FirtsName" varchar,
   "user_LastName"  varchar,
   primary key (id_project, id_user)
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table subscription
(
   id_subscription timeuuid primary key,
   "Enabled_At"    timestamp,
   "Expires_At"    timestamp,
   "Type"          varchar,
   id_order        timeuuid,
   id_user         timeuuid
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table user
(
   id_user      timeuuid primary key,
   address      varchar,
   country      varchar,
   email        varchar,
   firtsname    varchar,
   lastname     varchar,
   password     varchar,
   registeredat timestamp
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table user_configuration
(
   id_user          timeuuid,
   id_configuration timeuuid,
   app_color        varchar,
   app_font_size    int,
   app_font_type    varchar,
   bio_description  varchar,
   id_profile_image timeuuid,
   user_firstname   varchar,
   user_lastname    varchar,
   primary key (id_user, id_configuration)
)
   with comment = 'C7 = Mostrar configuracion de un usuario'
    and caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table user_invoices
(
   id_user        timeuuid,
   id_invoice     timeuuid,
   id_order       timeuuid,
   amount         float,
   user_firstname varchar,
   user_lastname  varchar,
   primary key (id_user, id_invoice, id_order)
)
   with comment = 'C6 = Mostrar lista de facturas por usuario'
    and caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE';

create table user_messages
(
   id_user         timeuuid,
   id_project      timeuuid,
   id_notification timeuuid,
   primary key (id_user, id_project, id_notification)
)
   with caching = {'keys': 'ALL', 'rows_per_partition': 'NONE'}
    and compaction = {'class': 'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy'}
    and compression = {'sstable_compression': 'org.apache.cassandra.io.compress.LZ4Compressor'}
    and speculative_retry = '99.0PERCENTILE
