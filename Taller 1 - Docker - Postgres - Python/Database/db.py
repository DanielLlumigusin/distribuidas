import psycopg2 as ps
from psycopg2 import sql

database = 'postgres'
user = 'postgres'
host = 'db'
password = 'P@SSWORD123'
port = 5432

connection = ps.connect(database=database, user=user, host=host, password=password, port=port)
if(not connection):
    print( "Error connecting to Database")
cursor = connection.cursor()