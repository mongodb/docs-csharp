Overview
--------

On this page, you can learn how to create ``UpdateDefinition`` objects for array fields.
An ``UpdateDefinition`` object specifies the kind of update operation to perform, the
fields to update, and the new value for each field, if applicable.

The {+driver-short+} supports the array update operators and modifiers described in the
:manual:`{+mdb-server+} manual </reference/operator/update/#array>`.
To create an ``UpdateDefinition`` object for one of these operators, call the corresponding
method from the ``Builders.Update`` property.
The following sections describe these methods in more detail.

After you create an ``UpdateDefinition`` object, pass it to the |sync-method|
or |async-method| method. For more information about these methods, see
the |update-page-link| page.

.. include:: /includes/method-overloads.rst

.. include:: /includes/atlas-sample-data.rst

Add One Value
-------------

To add one value to the end of an array, call the ``Builders.Update.Push()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add a value to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``value``
     - The value to add to the end of the array field. 

       **Data Type:** ``TItem`` 

The following code example uses the ``Push()`` method to add a new ``GradeEntry`` object
to the ``Grades`` array in |matching-document-or-documents|:

|push-code-example-tabs|

.. tip:: Configure the Push Operation
  
   To add a value at a specific position in an array, or to sort or slice the array after
   updating it, call the ``PushEach()`` method instead.

To add one value to the end of an array, *but only if it doesn't already exist in the array*,
call the ``Builders.Update.AddToSet()`` method. 
{+mdb-server+} determines whether the value already exists in the array by
comparing the value's BSON representation to the BSON representation of each
other element in the array.

The ``AddToSet()`` method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add a value to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``value``
     - The value to add to the end of the array field.

       **Data Type:** ``TItem`` 

The following code example calls the ``AddToSet()`` method to re-add the first
``GradeEntry`` object to the ``Grades`` array in |matching-document-or-documents|. Because
the value already exists in the array, the update operation does nothing.

|addtoset-code-example-tabs|

Add Multiple Values
-------------------

To add multiple values to an array, call the ``Builders.Update.PushEach()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add one or more values to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``values``
     - The values to add to the array field.

       **Data Type:** ``IEnumerable<TItem>``

   * - ``slice``
     - The number of elements to keep in the array, counted from the start of the array
       after updates. If the value is negative,
       the method keeps the specified number of elements from the end of the array.

       **Data Type:** ``int?``

   * - ``position``
     - The position in the array at which to add the values. By default, the method
       adds the values to the end of the array.

       **Data Type:** ``int?``

   * - ``sort``
     - A ``SortDefinition`` object that specifies how the driver sorts the array elements
       after adding the new values.

       **Data Type:** `SortDefinition<TItem> <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.SortDefinition-1.html>`__

The following code example uses the ``PushEach()`` method to add two new ``GradeEntry``
objects to the start of the ``Grades`` array in |matching-document-or-documents|.
It then sorts the array elements in descending order by the values of their ``Score`` fields.

|pusheach-code-example-tabs|   

To add multiple values to an array, *but only if they don't already exist in the array*,
call the ``Builders.Update.AddToSetEach()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add one or more values to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``values``
     - The values to add to the array field.

       **Data Type:** ``IEnumerable<TItem>``

The following code example calls the ``AddToSetEach()`` method to re-add the first
and second ``GradeEntry`` objects to the ``Grades`` array in |matching-document-or-documents|.
Because these values already exist in the array, the update operation does nothing.

|addtoseteach-code-example-tabs| 

Remove Values
-------------

To remove the first value from an array, call the ``Builders.Update.PopFirst()`` method.
This method accepts the following parameter:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to remove the first value from.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

The following code example uses the ``PopFirst()`` method to remove the first ``GradeEntry``
object from the ``Grades`` array in |matching-document-or-documents|:

|popfirst-code-example-tabs| 

To remove the last value from an array, call the ``Builders.Update.PopLast()`` method:
This method accepts the following parameter:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to remove the last value from.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

The following code example uses the ``PopLast()`` method to remove the last ``GradeEntry``
object from the ``Grades`` array in |matching-document-or-documents|:

|poplast-code-example-tabs|

To remove all instances of a specific value from an array, call the
``Builders.Update.Pull()`` method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to remove the values from.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``value``
     - The value to remove from the array field.

       **Data Type:** ``IEnumerable<TItem>``

The following code example uses the ``Pull()`` method to remove all instances of a
a specific ``GradeEntry`` object from the ``Grades`` array in |matching-document-or-documents|:

|pull-code-example-tabs|

To remove all instances of more than one specific value from an array, call the
``Builders.Update.PullAll()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to remove the values from.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``values``
     - The values to remove from the array field.

       **Data Type:** ``IEnumerable<TItem>``

The following code example uses the ``PullAll()`` method to remove all instances of two
specific ``GradeEntry`` objects from the ``Grades`` array in |matching-document-or-documents|:

|pullall-code-example-tabs|

To remove all values that match a specific condition from an array, call the
``Builders.Update.PullFilter()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to remove the values from.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``filter``
     - A query filter that specifies the condition for values to remove.

       **Data Type:** `FilterDefinition<TItem> <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.FilterDefinition-1.html>`__

The following code example uses the ``PullFilter()`` method to remove all ``GradeEntry``
objects where the ``Grade`` value is ``"F"`` from the ``Grades`` array in the
matching documents:

|pullfilter-code-example-tabs|

Update Matching Values
----------------------

To update a value in an array field, call the ``Builders.Update.Set()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to update.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``value``
     - The new value to set into the array field.

       **Data Type:** ``TItem``

You can use the
:manual:`positional operator </reference/operator/update/positional/#mongodb-update-up.->`
in combination with the ``Set()`` method to query and update specific values in the array.
If you're using the LINQ3 provider, the {+driver-short+} also supports LINQ syntax in
place of the positional operator.

The following sections describe different ways to update matching values in an array field.

First Matching Value
~~~~~~~~~~~~~~~~~~~~

To update only the first value in an array that matches a query filter, use the
positional operator (``$``) in combination with the ``Set()`` method.

.. note::
  
   To use the positional operator, the array field must be part of the query filter.

The following example uses the ``Set()`` method and the positional operator to
update the ``Grades`` array in |matching-document-or-documents|. First,
it finds *only the first* ``GradeEntry`` object in the ``Grades`` array where the ``Grade`` property
has the value ``"A"``. Then, it updates the ``Score`` property of the first matching
``GradeEntry`` object to 100.

|positional-code-example-tabs|

All Matching Values
~~~~~~~~~~~~~~~~~~~

To update all values in an array that match a specified condition, use the filtered
positional operator (``$[<identifier>]``) in combination with the ``Set()`` method.

The following example uses the ``Set()`` method and the filtered positional operator
to update the ``Score`` property of all matching
``GradeEntry`` objects in the ``Grades`` array to 100 in |matching-document-or-documents|:

|filteredpositional-code-example-tabs|

All Values
~~~~~~~~~~

To update all values in an array that match a query filter, use the all-positional operator
(``$[]``) in combination with the ``Set()`` method.

The following example uses the ``Set()`` method and the all-positional operator
to update the ``Score`` property of all ``GradeEntry`` objects in the ``Grades`` array
to 100 in |matching-document-or-documents|:

|allpositional-code-example-tabs|

API Documentation
-----------------

For more information about any of the methods or types discussed in this
guide, see the following API documentation:

- `Builders.Update.Push() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.Push.html>`__
- `Builders.Update.AddToSet() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.AddToSet.html>`__
- `Builders.Update.PushEach() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.PushEach.html>`__
- `Builders.Update.AddToSetEach() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.AddToSetEach.html>`__
- `Builders.Update.PopFirst() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.PopFirst.html>`__
- `Builders.Update.PopLast() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.PopLast.html>`__
- `Builders.Update.Pull() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.Pull.html>`__
- `Builders.Update.PullAll() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.PullAll.html>`__
- `Builders.Update.PullFilter() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.PullFilter.html>`__
- `Builders.Update.Set() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionBuilder-1.Set.html>`__