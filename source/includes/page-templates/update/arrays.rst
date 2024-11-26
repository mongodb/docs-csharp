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

After you create an ``UpdateDefinition`` object, pass it to the ``|sync-method|()``
or ``|sync-method|Async()`` method. For more information about these methods, see
the :ref:`<csharp-|file-folder|>` page.

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
     - An expression that specifies the array field to add to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``value``
     - The value to add to the end of the array field.

       **Data Type:** ``TItem`` 

The following code example calls the ``AddToSet()`` method to re-add the first
``GradeEntry`` object to the ``Grades`` array in |matching-document-or-documents|. Because
the value already exists in the array, the update operation does nothing.

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
     - An expression that specifies the array field to add to.

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
objects to the start of the ``Grades`` array. It then sorts the array elements in
descending order by the values of their ``Score`` fields.

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-pusheach-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pusheach
         :end-before: // end-update-one-pusheach

   .. tab:: UpdateOne (Async)
      :tabid: update-one-pusheach-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pusheach-async
         :end-before: // end-update-one-pusheach-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-pusheach-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pusheach
         :end-before: // end-update-many-pusheach

   .. tab:: UpdateMany (Async)
      :tabid: update-many-pusheach-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pusheach-async
         :end-before: // end-update-many-pusheach-async

To add multiple values to an array, *but only if they don't already exist in the array*,
call the ``Builders.Update.AddToSetEach()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``values``
     - The values to add to the array field.

       **Data Type:** ``IEnumerable<TItem>``

The following code example tries to use the ``AddToSetEach()`` method to re-add the first
and second ``GradeEntry`` objects to the ``Grades`` array in |matching-document-or-documents|.
Because these values already exist in the array, the update operation does nothing.

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-addtoseteach-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-addtoseteach
         :end-before: // end-update-one-addtoseteach

   .. tab:: UpdateOne (Async)
      :tabid: update-one-addtoseteach-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-addtoseteach-async
         :end-before: // end-update-one-addtoseteach-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-addtoset-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-addtoseteach
         :end-before: // end-update-many-addtoseteach

   .. tab:: UpdateMany (Async)
      :tabid: update-many-addtoset-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-addtoseteach-async
         :end-before: // end-update-many-addtoseteach-async

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

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-popfirst-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-popfirst
         :end-before: // end-update-one-popfirst

   .. tab:: UpdateOne (Async)
      :tabid: update-one-popfirst-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-popfirst-async
         :end-before: // end-update-one-popfirst-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-popfirst-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-popfirst
         :end-before: // end-update-many-popfirst

   .. tab:: UpdateMany (Async)
      :tabid: update-many-popfirst-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-popfirst-async
         :end-before: // end-update-many-popfirst-async

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

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-poplast-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-poplast
         :end-before: // end-update-one-poplast

   .. tab:: UpdateOne (Async)
      :tabid: update-one-poplast-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-poplast-async
         :end-before: // end-update-one-poplast-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-poplast-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-poplast
         :end-before: // end-update-many-poplast

   .. tab:: UpdateMany (Async)
      :tabid: update-many-poplast-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-poplast-async
         :end-before: // end-update-many-poplast-async

To remove all instances of a specific value from an array, call the ``Builders.Update.Pull()`` method:
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``value``
     - The value to remove from the array field.

       **Data Type:** ``IEnumerable<TItem>``

The following code example uses the ``Pull()`` method to remove all instances of a
a specific ``GradeEntry`` object from the ``Grades`` array in |matching-document-or-documents|:

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-pull-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pull
         :end-before: // end-update-one-pull

   .. tab:: UpdateOne (Async)
      :tabid: update-one-pull-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pull-async
         :end-before: // end-update-one-pull-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-pull-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pull
         :end-before: // end-update-many-pull

   .. tab:: UpdateMany (Async)
      :tabid: update-many-pull-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pull-async
         :end-before: // end-update-many-pull-async

To remove all instances of more than one specific value from an array, call the
``Builders.Update.PullAll()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``values``
     - The values to remove from the array field.

       **Data Type:** ``IEnumerable<TItem>``

The following code example uses the ``PullAll()`` method to remove all instances of two
specific ``GradeEntry`` objects from the ``Grades`` array in |matching-document-or-documents|:

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-pullall-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pullall
         :end-before: // end-update-one-pullall

   .. tab:: UpdateOne (Async)
      :tabid: update-one-pullall-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pullall-async
         :end-before: // end-update-one-pullall-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-pullall-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pullall
         :end-before: // end-update-many-pullall

   .. tab:: UpdateMany (Async)
      :tabid: update-many-pullall-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pullall-async
         :end-before: // end-update-many-pullall-async

To remove all values that match a specific condition from an array, call the
``Builders.Update.PullFilter()`` method.
This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the array field to add to.

       **Data Type:** ``Expression<Func<TDocument, IEnumerable<TItem>>>``

   * - ``filter``
     - A query filter that specifies the condition for values to remove.

       **Data Type:** `FilterDefinition<TItem> <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.FilterDefinition-1.html>`__

The following code example uses the ``PullFilter()`` method to remove all ``GradeEntry``
objects where the ``Grade`` property is ``"F"`` from the ``Grades`` array in the
matching documents:

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-pullfilter-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pullfilter
         :end-before: // end-update-one-pullfilter

   .. tab:: UpdateOne (Async)
      :tabid: update-one-pullfilter-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-pullfilter-async
         :end-before: // end-update-one-pullfilter-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-pullfilter-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pullfilter
         :end-before: // end-update-many-pullfilter

   .. tab:: UpdateMany (Async)
      :tabid: update-many-pullfilter-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-pullfilter-async
         :end-before: // end-update-many-pullfilter-async

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
     - The new value to set in the array field.

       **Data Type:** ``TItem``

You can use the
:manual:`positional operator </reference/operator/update/positional/#mongodb-update-up.->`
in combination with the ``Set()`` method to query and update specific values in the array.
The following sections describe different ways to use the positional operator.

First Matching Value
~~~~~~~~~~~~~~~~~~~~

To update only the first value in an array that matches a query filter, use the positional operator
(``$``) in combination with the ``Set()`` method.

.. note::
  
   To use the positional operator, the array field must be part of the query filter.

The following example uses the ``Set()`` method and the positional operator to
update the ``Grades`` array in all documents that match the query filter. First,
it finds *only the first* ``GradeEntry`` object in the ``Grades`` array where the ``Grade`` property
has the value ``"A"``. Then, it updates the ``Score`` property of the first matching
``GradeEntry`` object to 100.

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-positional-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-positional
         :end-before: // end-update-one-positional

   .. tab:: UpdateOne (Async)
      :tabid: update-one-positional-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-positional-async
         :end-before: // end-update-one-positional-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-positional-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-positional
         :end-before: // end-update-many-positional

   .. tab:: UpdateMany (Async)
      :tabid: update-many-positional-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-positional-async
         :end-before: // end-update-many-positional-async

All Matching Values
~~~~~~~~~~~~~~~~~~~

To update all values in an array that match a specified condition, use the filtered
positional operator (``$[<identifier>]``) in combination with the ``Set()`` method.

The following example uses the ``Set()`` method and the filtered positional operator
to update the ``Score`` property of all matching
``GradeEntry`` objects in the Grades`` array to 100 in all documents that match the
query filter.

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-allpositional-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-allpositional
         :end-before: // end-update-one-allpositional

   .. tab:: UpdateOne (Async)
      :tabid: update-one-allpositional-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-allpositional-async
         :end-before: // end-update-one-allpositional-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-allpositional-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-allpositional
         :end-before: // end-update-many-allpositional

   .. tab:: UpdateMany (Async)
      :tabid: update-many-allpositional-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-allpositional-async
         :end-before: // end-update-many-allpositional-async

All Values
~~~~~~~~~~

To update all values in an array that match a query filter, use the all-positional operator
(``$[]``) in combination with the ``Set()`` method.

The following example uses the ``Set()`` method and the all-positional operator
to update the ``Score`` property of all ``GradeEntry`` objects in the Grades`` array
to 100 in all documents that match the query filter.

.. tabs::

   .. tab:: UpdateOne (Sync)
      :tabid: update-one-filteredpositional-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-filteredpositional
         :end-before: // end-update-one-filteredpositional

   .. tab:: UpdateOne (Async)
      :tabid: update-one-filteredpositional-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-one-filteredpositional-async
         :end-before: // end-update-one-filteredpositional-async

   .. tab:: UpdateMany (Sync)
      :tabid: update-many-filteredpositional-sync

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-filteredpositional
         :end-before: // end-update-many-filteredpositional

   .. tab:: UpdateMany (Async)
      :tabid: update-many-filteredpositional-async

      .. literalinclude:: /includes/code-examples/UpdateArrays.cs
         :language: csharp
         :copyable: true
         :dedent:
         :start-after: // start-update-many-filteredpositional-async
         :end-before: // end-update-many-filteredpositional-async


LINQ3 Provider
~~~~~~~~~~~~~~

LINQ syntax contains a positional operator (``$``) that you can use to update elements in an array field.
Previous versions of the {+driver-short+} supported both the LINQ2 and LINQ3 providers.
In LINQ2, you could use ``-1`` to indicate use of the positional operator.

For example, the ``Restaurant`` class contains an array field named ``Grades`` that
contains multiple ``GradeEntry`` elements. If your application were using the LINQ2
provider, you could use the following code sample to update the
``Grade`` field of the first element in this array:

.. code-block:: csharp
   :linenos:

   var anArrayId = ObjectId.GenerateNewId();
   var another = new Restaurant
   {
       Id = ObjectId.GenerateNewId(),
       AnArrayMember = new List<AnArrayClass>
       {
           new AnArrayClass { Id = anArrayId, Deleted = false }
       }
   };

   await collection.UpdateOneAsync(
      r => r.Id == "targetId" && r.AnArrayMember.Any(l => l.Id == anArrayId),
      Builders<Restaurant>.Update.Set(l => l.AnArrayMember.ElementAt(-1).Deleted, true));

.. code-block:: csharp
   :linenos:

   var anArrayId = ObjectId.GenerateNewId();
   var another = new Restaurant
   {
       Id = ObjectId.GenerateNewId(),
       AnArrayMember = new List<AnArrayClass>
       {
           new AnArrayClass { Id = anArrayId, Deleted = false }
       }
   };

   await collection.UpdateOneAsync(
      r => r.Id == "targetId" && r.AnArrayMember.Any(l => l.Id == anArrayId),
      Builders<Restaurant>.Update.Set(l => l.AnArrayMember.ElementAt(-1).Deleted, true));

This no longer works in LINQ3. Instead, you must use the following syntax: