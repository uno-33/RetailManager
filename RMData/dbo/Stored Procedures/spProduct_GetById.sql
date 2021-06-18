CREATE PROCEDURE [dbo].[spProduct_GetById]
@id int = 0

AS
begin
	set nocount on;

	select Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	from dbo.Product
	where Id = @id;
end
